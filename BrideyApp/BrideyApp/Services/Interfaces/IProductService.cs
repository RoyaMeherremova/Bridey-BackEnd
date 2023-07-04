using BrideyApp.Models;
using BrideyApp.ViewModels.Product;

namespace BrideyApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetFullDataById(int? id);
        Task<Product> GetById(int? id);
        Task<int> GetCountAsync();
        Task<List<ProductVM>> GetMappedAllProducts();
        Task<List<Product>> GetFeaturedProducts();
        Task<List<Product>> GetLatestProducts();
        Task<List<Product>> GetPaginatedDatas(int page, int take, int? cateId,int? compositionId, int? sizeId, int? colorId, int? brandId);
        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByCompositionIdAsync(int? id, int page = 1, int take = 6);
        Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 6);
        Task<int> GetProductsCountByCategoryAsync(int? catId);
        Task<int> GetProductsCountByCompositionAsync(int? catId);
        Task<int> GetProductsCountByColorAsync(int? colorId);
        Task<int> GetProductsCountByBrandAsync(int? tagid);
        Task<int> GetProductsCountBySizeAsync(int? tagid);

        Task<ProductImage> GetImageById(int? id);
        Task<Product> GetProductByImageId(int? id);

    }
}
