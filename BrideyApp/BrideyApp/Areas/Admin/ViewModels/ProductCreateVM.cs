using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int Rate { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int SaleCount { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<IFormFile> Photos { get; set; }
        public string Video { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> ColorIds { get; set; } = new();

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CategoryIds { get; set; } = new();

        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> SizeIds { get; set; } = new();
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CompositionIds { get; set; } = new();
    }
}
