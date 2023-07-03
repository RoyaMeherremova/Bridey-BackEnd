using System.Drawing;

namespace BrideyApp.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; } = 5;
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string SKU { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductComposition> ProductCompositions { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

        public string Video { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        //public ICollection<ProductBasket> ProductBaskets { get; set; }
    }
}
