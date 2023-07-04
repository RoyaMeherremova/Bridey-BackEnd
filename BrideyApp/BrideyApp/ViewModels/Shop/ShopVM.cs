using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.ViewModels.Product;

namespace BrideyApp.ViewModels.Shop
{
    public class ShopVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Color> Colors { get; set; }
        public List<Composition> Compositions { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public Sale Sale { get; set; }
        public Paginate<ProductVM> PaginateDatas { get; set; }



    }
}
