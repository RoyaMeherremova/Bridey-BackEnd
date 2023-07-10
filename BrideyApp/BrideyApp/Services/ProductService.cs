using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace BrideyApp.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.Include(m => m.Images)
                                                                    .Include(m => m.ProductSizes)
                                                                    .ThenInclude(m => m.Size)
                                                                    .Include(m => m.Brand)
                                                                    .Include(m => m.ProductCompositions)
                                                                    .ThenInclude(m => m.Composition)
                                                                    .Include(m => m.ProductColors)
                                                                    .ThenInclude(m => m.Color)
                                                                    .Include(m => m.ProductComments)
                                                                    .Include(m => m.ProductCategories)
                                                                    .ThenInclude(m => m.Category)
                                                                    .ToListAsync();
        public async Task<Product> GetById(int? id) => await _context.Products.Include(p => p.Images)
                                                                               .Include(m => m.ProductSizes)
                                                                               .Include(m => m.ProductCategories)
                                                                               .Include(m => m.ProductColors)
                                                                               .Include(m => m.ProductCompositions)
                                                                               .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> GetFullDataById(int? id) => await _context.Products.Include(m => m.Images)
                                                                    .Include(m => m.ProductSizes)
                                                                    .ThenInclude(m => m.Size)
                                                                    .Include(m => m.Brand)
                                                                    .Include(m => m.ProductCompositions)
                                                                    .ThenInclude(m => m.Composition)
                                                                    .Include(m => m.ProductColors)
                                                                    .ThenInclude(m => m.Color)
                                                                    .Include(m => m.ProductComments)
                                                                    .Include(m => m.ProductCategories)
                                                                    .ThenInclude(m => m.Category)
                                                                    .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<List<ProductVM>> GetMappedAllProducts()
        {
            List<ProductVM> model = new();
            var products = await _context.Products.Include(p => p.Images).ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    Name = item.Name,
                    Images = item.Images,
                    Rate= item.Rate,
                    Video = item.Video,
                });
            }
            return model;
        }
        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();
        public async Task<List<Product>> GetFeaturedProducts() => await _context.Products.Include(m => m.Images).OrderByDescending(m => m.Rate).ToListAsync();
        public async Task<List<Product>> GetLatestProducts() => await _context.Products.Include(m => m.Images).OrderByDescending(m => m.CreatedDate).ToListAsync();
        public async Task<List<Product>> GetPaginatedDatas(int page, int take, string sortValue,string searchText,int? cateId, int? compositionId, int? sizeId, int? colorId, int? brandId, int? value1, int? value2)
        {
            List<Product> products = products = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .Include(p => p.ProductCompositions)
            .ThenInclude(pt => pt.Composition)
            .Include(p => p.ProductSizes)
            .ThenInclude(ps => ps.Size)
            .Include(p => p.ProductColors)
            .ThenInclude(ps => ps.Color)
            .Include(p => p.Brand)
            .Skip((page * take) - take)
            .Take(take)
            .ToListAsync();
            if (sortValue != null)
            {
                if (sortValue == "1")
                {
                    products = await _context.Products.Include(m => m.Images).Skip((page * take) - take).Take(take).ToListAsync();
                };
                if (sortValue == "2")
                {
                    products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.SaleCount).Skip((page * take) - take).Take(take).ToListAsync();

                };
                if (sortValue == "3")
                {
                    products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Rate).Skip((page * take) - take).Take(take).ToListAsync();

                };
                if (sortValue == "4")
                {
                    products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.CreatedDate).Skip((page * take) - take).Take(take).ToListAsync();

                };
                if (sortValue == "5")
                {
                    products = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Price).Skip((page * take) - take).Take(take).ToListAsync();

                };
                if (sortValue == "6")
                {
                    products = await _context.Products.Include(m => m.Images).OrderBy(p => p.Price).Skip((page * take) - take).Take(take).ToListAsync();

                };
           
            }
            if (searchText != null)
            {
                products = await _context.Products
                .Include(p => p.Images)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (cateId != null)
            {
                products = await _context.ProductCategories
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Category.Id == cateId)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (compositionId != null)
            {
                products = await _context.ProductCompositions
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Composition.Id == compositionId)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (sizeId != null)
            {
                products = await _context.ProductSizes.Where(m=>m.SizeId == sizeId).Include(p => p.Product)
                                                                     .ThenInclude(p => p.Images)
                                                                     .Select(p => p.Product)
                                                                     .Skip((page * take) - take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            }
            if (colorId != null)
            {
                products = await _context.ProductColors
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Color.Id == colorId)
                .Select(p => p.Product)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
            }
            if (brandId != null)
            {
             products = await _context.Products
            .Include(p => p.Images)
            .Include(c => c.Brand)
            .Where(p => p.Brand.Id == brandId)
            .Skip((page * take) - take)
            .Take(take)
            .ToListAsync();

            }
            if (value1 != null && value2 != null)
            {
                products = await _context.Products
               .Include(p => p.Images)
               .Where(p => p.Price >= value1 && p.Price <= value2)
               .Skip((page * take) - take)
               .Take(take)
               .ToListAsync();

            }

            return products;
        }


        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.ProductCategories.Include(p => p.Product)
                                                                     .ThenInclude(p => p.Images)
                                                                     .Where(pc => pc.Category.Id == id)
                                                                     .Select(p => p.Product)
                                                                     .Skip((page * take) - take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Images = product.Images,
                    Rate = product.Rate,
                    Video = product.Video
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.ProductColors.Include(p => p.Product)
                                                                     .ThenInclude(p => p.Images)
                                                                     .Where(pc => pc.Color.Id == id)
                                                                     .Select(p => p.Product)
                                                                     .Skip((page * take) - take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Images = product.Images,
                    Rate = product.Rate,
                    Video = product.Video

                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.ProductSizes.Include(p => p.Product)
                                                                     .ThenInclude(p => p.Images)
                                                                     .Where(pc => pc.Size.Id == id)
                                                                     .Select(p => p.Product)
                                                                     .Skip((page * take) - take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Images = product.Images,
                    Rate = product.Rate,
                    Video = product.Video

                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByCompositionIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.ProductCompositions.Include(p => p.Product)
                                                                     .ThenInclude(p => p.Images)
                                                                     .Where(pc => pc.Composition.Id == id)
                                                                     .Select(p => p.Product)
                                                                     .Skip((page * take) - take)
                                                                     .Take(take)
                                                                     .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Images = product.Images,
                    Rate = product.Rate,
                    Video = product.Video

                });
            }
            return model;
        }
        public async Task<int> GetProductsCountByRangeAsync(int? value1, int? value2)
        {
            return await _context.Products.Where(p => p.Price >= value1 && p.Price <= value2)
                                 .Include(p => p.Images)
                                 .CountAsync();
        }
        public async Task<int> GetProductsCountBySearchTextAsync(string searchText)
        {
            return await _context.Products.Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                                 .Include(p => p.Images)
                                 .CountAsync();
        }
        public async Task<int> GetProductsCountBySortTextAsync(string sortValue)
        {
            int count = 0;
            if (sortValue == "1")
            {
                return  await _context.Products.Include(m => m.Images).CountAsync(); 
            };
            if (sortValue == "2")
            {
                count = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.SaleCount).CountAsync();

            };
            if (sortValue == "3")
            {
                count = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Rate).CountAsync();

            };
            if (sortValue == "4")
            {
                count = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.CreatedDate).CountAsync();

            };
            if (sortValue == "5")
            {
                count = await _context.Products.Include(m => m.Images).OrderByDescending(p => p.Price).CountAsync();

            };
            if (sortValue == "6")
            {
                count = await _context.Products.Include(m => m.Images).OrderBy(p => p.Price).CountAsync();

            };

            return count;

        }
        public async Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 9)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.Products.Include(p => p.Images)
                                                            .Include(c => c.Brand)
                                                            .Where(pc => pc.Brand.Id == id)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Price = product.Price,
                    Name = product.Name,
                    Images = product.Images,
                    Rate = product.Rate,
                    Video = product.Video

                });
            }
            return model;
        }
        public async Task<int> GetProductsCountByCategoryAsync(int? id)
        {
            return await _context.ProductCategories
                 .Include(p => p.Product)
                 .Include(c => c.Category)
                 .Where(pc => pc.Category.Id == id)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountByColorAsync(int? id)
        {
            return await _context.ProductColors
                 .Include(p => p.Product)
                 .Where(pc => pc.Color.Id == id)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountBySizeAsync(int? id)
        {
            return await _context.ProductSizes
                 .Include(p => p.Product)
                 .Where(pc => pc.Size.Id == id)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountByCompositionAsync(int? id)
        {
            return await _context.ProductCompositions
                 .Include(p => p.Product)
                 .Where(pc => pc.Composition.Id == id)
                 .Select(p => p.Product)
                 .CountAsync();
        }
        public async Task<int> GetProductsCountByBrandAsync(int? id)
        {
            return await _context.Products
                 .Include(p => p.Brand)
                 .Where(pc => pc.Brand.Id == id)
                 .CountAsync();
        }

        public async Task<ProductImage> GetImageById(int? id)
        {
            return await _context.ProductImages.FindAsync((int)id);
        }

        public async Task<Product> GetProductByImageId(int? id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Images.Any(p => p.Id == id));
        }
        public async Task<List<Product>> GetAllBySearchText(string searchText)
        {
            var products = await _context.Products
                .Include(p => p.Images)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
            return products;
        }
        public async Task<List<ProductComment>> GetComments()
        {
            return await _context.ProductComments.Include(p => p.Product).ToListAsync();
        }

        public async Task<ProductComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.ProductComments.Include(p => p.Product).FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProductComment> GetCommentById(int? id)
        {
            return await _context.ProductComments.FirstOrDefaultAsync(pc => pc.Id == id);
        }

    }
}
