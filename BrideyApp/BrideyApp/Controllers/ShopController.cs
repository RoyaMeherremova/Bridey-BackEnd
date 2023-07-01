using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BrideyApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly ICompositionService _compositionService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;


        public ShopController(ILayoutService layoutService,
                              ISizeService sizeService, 
                              IColorService colorService,
                              ICompositionService compositionService,
                              IBrandService brandService,
                              ICategoryService categoryService,
                              AppDbContext context,
                              IProductService productService)
        {
            _layoutService = layoutService;
            _sizeService = sizeService;
            _colorService = colorService;
            _compositionService = compositionService;
            _brandService = brandService;
            _categoryService = categoryService;
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 9, int? cateId =null, int? compositionId = null)
        {
            List<Product> paginateProducts = await _productService.GetPaginatedDatas(page, take, cateId, compositionId);
            List<ProductVM> mappedDatas = GetMappedDatas(paginateProducts);

            int pageCount = 0;
            if (cateId != null)
            {
                pageCount = await GetPageCountAsync(take, cateId, null, null,null,null);
            }
            if (compositionId != null)
            {
                pageCount = await GetPageCountAsync(take, null,compositionId, null, null,null);
            }
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            List<Size> sizes = await _sizeService.GetAll();
            List<Color> colors = await _colorService.GetAll();
            List<Brand> brands = await _brandService.GetAll();
            Sale sale = await _context.Sales.FirstOrDefaultAsync();
            List<Category> categories = await _categoryService.GetAll();
            List<Composition> compositions = await _compositionService.GetAll();



            ShopVM model = new()
            {
                SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                Sizes= sizes,
                Colors= colors,
                Compositions = compositions,
                Brands = brands,
                Categories = categories,
                Sale= sale,
                PaginateDatas = paginatedDatas,
            };
            return View(model);
        }


        private List<ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ProductVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Images = product.Images,
                    Rate = product.Rate
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }
        private async Task<int> GetPageCountAsync(int take, int? catId, int? compositionId, int? colorId, int? brandId, int? sizeId)
        {
            int prodCount = 0;
            if (catId is not null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
            }
            if (colorId is not null)
            {
                prodCount = await _productService.GetProductsCountByColorAsync(colorId);

            }
            if (brandId is not null)
            {
                prodCount = await _productService.GetProductsCountByBrandAsync(brandId);

            }
            if (sizeId is not null)
            {
                prodCount = await _productService.GetProductsCountBySizeAsync(sizeId);

            }
            if (compositionId is not null)
            {
                prodCount = await _productService.GetProductsCountByCompositionAsync(compositionId);

            }
            if (catId == null && compositionId == null && colorId == null)
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }
    }
}
