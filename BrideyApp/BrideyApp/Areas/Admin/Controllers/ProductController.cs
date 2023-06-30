using BrideyApp.Areas.Admin.ViewModels;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using Color = BrideyApp.Models.Color;
using Size = BrideyApp.Models.Size;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ICompositionService _compositionService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        public ProductController(AppDbContext context, 
                                 IWebHostEnvironment env,
                                 IProductService productService, 
                                 IBrandService brandService,
                                 ICategoryService categoryService,
                                 ICompositionService compositionService,
                                 IColorService colorService,
                                 ISizeService sizeService)
        {
            _context = context;
            _env = env;
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _compositionService = compositionService;
            _colorService = colorService;
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> datas = await _productService.GetPaginatedDatas(page, take, null, null, null, null, null);
            List<ProductListVM> mappedDatas = GetMappedDatas(datas);
            int pageCount = await GetPageCountAsync(take);
            ViewBag.take = take;
            Paginate<ProductListVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return View(paginatedDatas);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        private List<ProductListVM> GetMappedDatas(List<Product> products)
        {
            List<ProductListVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductListVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Images = product.Images,
                    SKU = product.SKU
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.colors = await GetColorsAsync();
            ViewBag.compositions = await GetCompositionsAsync();
            ViewBag.sizes = await GetSizesAsync();
            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.brands = await GetBrandsAsync();
            return View();
        }
        private async Task<SelectList> GetColorsAsync()
        {
            IEnumerable<Color> colors = await _colorService.GetAll();
            return new SelectList(colors, "Id", "Name");
        }
        private async Task<SelectList> GetCompositionsAsync()
        {
            IEnumerable<Composition> compositions = await _compositionService.GetAll();
            return new SelectList(compositions, "Id", "Name");
        }
        private async Task<SelectList> GetSizesAsync()
        {
            IEnumerable<Size> sizes = await _sizeService.GetAll();
            return new SelectList(sizes, "Id", "Name");
        }
        private async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }
        private async Task<SelectList> GetBrandsAsync()
        {
            IEnumerable<Brand> brands = await _brandService.GetAll();
            return new SelectList(brands, "Id", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            try
            {
                ViewBag.colors = await GetColorsAsync();
                ViewBag.compositions = await GetCompositionsAsync();
                ViewBag.sizes = await GetSizesAsync();
                ViewBag.brands = await GetBrandsAsync();
                ViewBag.categories = await GetCategoriesAsync();

                if (!ModelState.IsValid) return View(model);

                Product newProduct = new();
                List<ProductImage> productImages = new();
                List<ProductComposition> productCompositions = new();
                List<ProductSize> productSizes = new();
                List<ProductCategory> productCategories = new();
                List<ProductColor> productColors = new();

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File type must be image");
                        return View(model);
                    }
                    if (!photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photos", "Image size must be max 500kb");
                        return View(model);
                    }
                }

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);
                }
                newProduct.Images = productImages;
                newProduct.Images.FirstOrDefault().IsMain = true;
                if (model.ColorIds.Count > 0)
                {
                    foreach (var item in model.ColorIds)
                    {
                        ProductColor productColor = new()
                        {
                            ColorId = item
                        };
                        productColors.Add(productColor);
                    }
                    newProduct.ProductColors = productColors;
                }
                else
                {
                    ModelState.AddModelError("Colors", "Don`t be empty");
                    return View(model);
                }
                if (model.CompositionIds.Count > 0)
                {
                    foreach (var item in model.CompositionIds)
                    {
                        ProductComposition productComposition = new()
                        {
                            CompositionId = item
                        };
                        productCompositions.Add(productComposition);
                    }
                    newProduct.ProductCompositions = productCompositions;
                }
                else
                {
                    ModelState.AddModelError("Compositions", "Don`t be empty");
                    return View(model);
                }

                if (model.SizeIds.Count > 0)
                {
                    foreach (var item in model.SizeIds)
                    {
                        ProductSize productSize = new()
                        {
                            SizeId = item
                        };
                        productSizes.Add(productSize);
                    }
                    newProduct.ProductSizes = productSizes;
                }
                else
                {
                    ModelState.AddModelError("Sizes", "Don`t be empty");
                    return View(model);
                }

                if (model.CategoryIds.Count > 0)
                {
                    foreach (var item in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };
                        productCategories.Add(productCategory);
                    }
                    newProduct.ProductCategories = productCategories;
                }
                else
                {
                    ModelState.AddModelError("Categories", "Don`t be empty");
                    return View(model);
                }
                var convertedPrice = decimal.Parse(model.Price);
                Random random = new();

                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = convertedPrice;
                newProduct.StockCount = model.StockCount;
                newProduct.SKU = model.Name.Substring(0, 3) + "-" + random.Next();
                newProduct.BrandId = model.BrandId;
                newProduct.Rate = model.Rate;
                newProduct.SaleCount = model.SaleCount;
                newProduct.Video = model.Video;

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id, int page)
        {
            try
            {
                if (id is null) return BadRequest();
                Product dbProduct = await _productService.GetFullDataById((int)id);
                if (dbProduct is null) return NotFound();
                ViewBag.page = page;

                ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    SKU = dbProduct.SKU,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    StockCount = dbProduct.StockCount,
                    SaleCount = dbProduct.SaleCount,
                    Images = dbProduct.Images,
                    ProductCategories = dbProduct.ProductCategories,
                    ProductCompositions = dbProduct.ProductCompositions,
                    ProductColors = dbProduct.ProductColors,
                    ProductSizes = dbProduct.ProductSizes,
                    Brand = dbProduct.Brand.Image,
                    Video = dbProduct.Video,
                    Rate = dbProduct.Rate,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

          


    }
}
