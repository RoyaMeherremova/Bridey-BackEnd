namespace BrideyApp.Models
{
    public class Category:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
