using BrideyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        public string SKU { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int SaleCount { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Video { get; set; }
        public List<IFormFile> Photos { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public int BrandId { get; set; }
        public ICollection<ProductComposition> ProductCompositions { get; set; }
        public List<int> CompositionIds { get; set; } = new();
        public ICollection<ProductColor> ProductColors { get; set; }
        public List<int> ColorIds { get; set; } = new();
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public ICollection<ProductSize> ProductSizes { get; set; }
        public List<int> SizeIds { get; set; } = new();
    }
}
