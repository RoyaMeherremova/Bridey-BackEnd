using BrideyApp.Models;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SaleCount { get; set; }
        public int Rate { get; set; }
        public int StockCount { get; set; }
        public string Brand { get; set; }
        public string Video { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductComposition> ProductCompositions { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
