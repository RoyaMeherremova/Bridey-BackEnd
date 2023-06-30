using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAll() => await _context.Products.Include(m => m.Images)
                                                                    .Include(m => m.ProductSizes)
                                                                    .ThenInclude(m => m.Size)
                                                                    .Include(m => m.Brand)
                                                                    .Include(m => m.ProductCompositions)
                                                                    .ThenInclude(m => m.Composition)
                                                                    .Include(m => m.ProductColors)
                                                                    .ThenInclude(m => m.Color)
                                                                    //.Include(m => m.ProductComments)
                                                                    .Include(m => m.ProductCategories)
                                                                    .ThenInclude(m => m.Category)
                                                                    .ToListAsync();
        public async Task<Product> GetById(int? id) => await _context.Products.FindAsync(id);
        public async Task<Product> GetFullDataById(int? id) => await _context.Products.Include(m => m.Images)
                                                                    .Include(m => m.ProductSizes)
                                                                    .ThenInclude(m => m.Size)
                                                                    .Include(m => m.Brand)
                                                                    .Include(m => m.ProductCompositions)
                                                                    .ThenInclude(m => m.Composition)
                                                                    .Include(m => m.ProductColors)
                                                                    .ThenInclude(m => m.Color)
                                                                    //.Include(m => m.ProductComments)
                                                                    .Include(m => m.ProductCategories)
                                                                    .ThenInclude(m => m.Category)
                                                                    .FirstOrDefaultAsync(m => m.Id == id);
        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();
        public async Task<List<Product>> GetFeaturedProducts() => await _context.Products.Include(m => m.Images).OrderByDescending(m => m.Rate).ToListAsync();
        public async Task<List<Product>> GetLatestProducts() => await _context.Products.Include(m => m.Images).OrderByDescending(m => m.CreatedDate).ToListAsync();
        public async Task<List<Product>> GetPaginatedDatas(int page, int take, int? cateId, int? compositionId, int? sizeId, int? colorId, int? brandId)
        {
            List<Product> products = null;

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
                products = await _context.ProductSizes
                .Include(p => p.Product)
                .ThenInclude(p => p.Images)
                .Where(pc => pc.Size.Id == sizeId)
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
            else
            {
             products = await _context.Products
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
            }
            return products;
        }
        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 6)
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
                    Rate = product.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 6)
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
                    Rate = product.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 6)
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
                    Rate = product.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByCompositionIdAsync(int? id, int page = 1, int take = 6)
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
                    Rate = product.Rate
                });
            }
            return model;
        }
        public async Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 6)
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
                    Rate = product.Rate
                });
            }
            return model;
        }
        public async Task<int> GetProductsCountByCategoryAsync(int? id)
        {
            return await _context.ProductCategories
                 .Include(p => p.Product)
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

    }
}
