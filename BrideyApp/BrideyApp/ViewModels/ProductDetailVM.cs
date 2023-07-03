using BrideyApp.Models;

namespace BrideyApp.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<Social> Socials { get; set; }
        public List<Product> FeaturedProducts { get; set; }

        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductComposition> ProductCompositions { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public string Brand { get; set; }
        public ProductCommentVM ProductCommentVM { get; set; }
    }
}
