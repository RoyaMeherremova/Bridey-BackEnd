using BrideyApp.Models;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
