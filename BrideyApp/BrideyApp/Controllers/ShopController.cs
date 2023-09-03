using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Cart;
using BrideyApp.ViewModels.Product;
using BrideyApp.ViewModels.Shop;
using BrideyApp.ViewModels.Wishlist;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using Color = BrideyApp.Models.Color;
using Size = BrideyApp.Models.Size;

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
        private readonly IHeaderBackgroundService _headerBackgroundService;
        private readonly ISocialService _socialService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;



        public ShopController(ILayoutService layoutService,
                              ISizeService sizeService, 
                              IColorService colorService,
                              ICompositionService compositionService,
                              IBrandService brandService,
                              ICategoryService categoryService,
                              AppDbContext context,
                              IProductService productService,
                              IHeaderBackgroundService headerBackgroundService,
                              ISocialService socialService,
                              ICartService cartService,
                              IWishlistService wishlistService)
        {
            _layoutService = layoutService;
            _sizeService = sizeService;
            _colorService = colorService;
            _compositionService = compositionService;
            _brandService = brandService;
            _categoryService = categoryService;
            _context = context;
            _productService = productService;
            _headerBackgroundService = headerBackgroundService;
            _socialService = socialService;
            _cartService = cartService;
            _wishlistService= wishlistService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 9, string sortValue = null,string searchText = null, int? cateId =null, int? compositionId = null, int? sizeId = null, int? brandId = null, int? colorId = null, int? value1 =null, int? value2 = null)
        {
            List<Product> paginateProducts = await _productService.GetPaginatedDatas(page, take, sortValue, searchText, cateId, compositionId,sizeId,colorId,brandId,value1,value2);
            List<ProductVM> mappedDatas = GetMappedDatas(paginateProducts);
            ViewBag.cateId = cateId;
            ViewBag.compositionId = compositionId;
            ViewBag.sizeId = sizeId;
            ViewBag.colorId = colorId;
            ViewBag.brandId = brandId;
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.searchText = searchText;
            ViewBag.sortValue = sortValue;




            int pageCount = 0;
            if (cateId != null)
            {
                pageCount = await GetPageCountAsync(take,null,null, cateId, null, null,null,null,null,null);
            }
            if (compositionId != null)
            {
                pageCount = await GetPageCountAsync(take,null, null,null,compositionId, null, null,null, null, null);
            }
            if (sizeId != null)
            {
                pageCount = await GetPageCountAsync(take,null, null,null,null, null, null, sizeId, null, null);
            }
            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take,null,null, null, null, null, brandId, null, null, null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take,null,null, null, null, colorId, null, null, null, null);
            }
            if (value1 != null && value2 != null)
            {
                pageCount = await GetPageCountAsync(take,null,null, null, null, null, null, null, value1, value2);
            }
            if (searchText !=null)
            {
                pageCount = await GetPageCountAsync(take,null, searchText, null, null, null, null, null, null, null);

            }
            if (sortValue != null)
            {
                pageCount = await GetPageCountAsync(take, sortValue,null, null, null, null, null, null, null, null);

            }

            if (sortValue == null && searchText == null &&colorId == null && compositionId == null &&  cateId == null && brandId == null && sizeId == null && value1 == null && value2 == null)
            {
                pageCount = await GetPageCountAsync(take,null, null,null, null, null, null, null, null, null);
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
                Sizes = sizes,
                Colors = colors,
                Compositions = compositions,
                Brands = brands,
                Categories = categories,
                Sale = sale,
                PaginateDatas = paginatedDatas,
                CountProducts = await _productService.GetCountAsync()
               
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
                    Images = product.Images.ToList(),
                    Rate = product.Rate,
                    Video = product.Video,
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }
        private async Task<int> GetPageCountAsync(int take, string sortValue,string searchText, int? catId, int? compositionId, int? colorId, int? brandId, int? sizeId,int? value1,int? value2)
        {
            int prodCount = 0;
            if (sortValue is not null)
            {
                prodCount = await _productService.GetProductsCountBySortTextAsync(sortValue);
            }
            if (searchText is not null)
            {
                prodCount = await _productService.GetProductsCountBySearchTextAsync(searchText);
            }
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
            if (value1 != null && value2 != null)
            {
                prodCount = await _productService.GetProductsCountByRangeAsync(value1, value2);
            }


            if (sortValue == null && searchText == null &&catId == null && compositionId == null && colorId == null && brandId == null && sizeId == null && value1 == null && value2 == null)
            {
                prodCount = await _productService.GetCountAsync();
            }
          
            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int page =1,int take=9)
        {
            int pageCount = await GetPageCountAsync(take,null, null,null, null, null, null, null, null, null);
            var products =  await _productService.GetPaginatedDatas(page, take,null, null,null, null, null, null, null,null,null);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return PartialView("_ProductsPartial", paginatedDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetRangeProducts(int value1, int value2, int page = 1, int take = 9)
        {
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;


            List<Product> products = await _context.Products.Where(x => x.Price >= value1 && x.Price <= value2).Include(m=>m.Images).ToListAsync();
            var productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return PartialView("_ProductsPartial", paginatedDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.cateId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id,page,take);  

            int pageCount = await GetPageCountAsync(take,null,null,(int)id, null, null, null, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;

            var products = await _productService.GetProductsByColorIdAsync(id);

            int pageCount = await GetPageCountAsync(take,null,null, null, null, (int)id, null, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsBySize(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.sizeId = id;

            var products = await _productService.GetProductsBySizeIdAsync(id);

            int pageCount = await GetPageCountAsync(take,null,null, null, null, null, null, (int)id, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByComposition(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.compositionId = id;

            var products = await _productService.GetProductsByCompositionIdAsync(id);

            int pageCount = await GetPageCountAsync(take,null,null, null, (int)id, null, null, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 9)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;

            var products = await _productService.GetProductsByBrandIdAsync(id);

            int pageCount = await GetPageCountAsync(take,null,null, null, null, null, (int)id, null,null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetFullDataById((int)id);
                if (dbProduct is null) return NotFound();

                ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    ProductCategories = dbProduct.ProductCategories,
                    ProductCompositions = dbProduct.ProductCompositions,
                    ProductColors = dbProduct.ProductColors,
                    ProductSizes = dbProduct.ProductSizes,
                    Images = dbProduct.Images,
                    SKU = dbProduct.SKU,
                    Rate = dbProduct.Rate,
                    Description = dbProduct.Description,
                    Brand = dbProduct.Brand.Name,
                    Video = dbProduct.Video,
                    SectionBackgroundImages = _layoutService.GetSectionBackgroundImages(),
                    HeaderBackgrounds = _headerBackgroundService.GetHeaderBackgroundDatas(),
                    Socials = await _socialService.GetAll(),
                    FeaturedProducts = await _productService.GetFeaturedProducts(),
                    ProductCommentVM = new(),
                    ProductComments = dbProduct.ProductComments.ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ProductDetailVM model, int? id, string userId)
        {
            if (id is null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(ProductDetail), new { id });

            ProductComment productComment = new()
            {
                Name = model.ProductCommentVM.Name,
                Email = model.ProductCommentVM.Email,
                Message = model.ProductCommentVM.Message,
                AppUserId = userId,
                ProductId = (int)id
            };
            await _context.ProductComments.AddAsync(productComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProductDetail), new { id });
        }

        public async Task<IActionResult> Sort(string? sortValue, int page = 1, int take = 9)
        {
            ViewBag.sortValue = sortValue;

            List<Product> products = new();

            if (sortValue == "1")
            {
                products = await _context.Products.Include(m=>m.Images).ToListAsync();
            };
            if (sortValue == "2")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.SaleCount).ToListAsync();

            };
            if (sortValue == "3")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Rate).ToListAsync();

            };
            if (sortValue == "4")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.CreatedDate).ToListAsync();

            };
            if (sortValue == "5")
            {
                products = await _context.Products.Include(m => m.Images).OrderBy(p => p.Price).ToListAsync();

            };
            if (sortValue == "6")
            {
                products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Price).ToListAsync();

            };
            int productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = GetMappedDatas(products);
            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return PartialView("_ProductsPartial", model);
        }



        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetById((int)id);

            if (dbProduct == null) return NotFound();

            List<CartVM> carts = _cartService.GetDatasFromCookie();

            CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

            _cartService.SetDatasToCookie(carts, dbProduct, existProduct);
           
            int cartCount = carts.Count;

            return Ok(cartCount);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetById((int)id);

            if (dbProduct == null) return NotFound();

            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            WishlistVM existProduct = wishlists.FirstOrDefault(p => p.ProductId == id);

            _wishlistService.SetDatasToCookie(wishlists, dbProduct, existProduct);

            int wishlistCount = wishlists.Count;

            return Ok(wishlistCount);
        }

    


    }
}
