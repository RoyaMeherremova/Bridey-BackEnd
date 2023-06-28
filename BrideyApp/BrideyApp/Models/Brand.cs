namespace BrideyApp.Models
{
    public class Brand:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
