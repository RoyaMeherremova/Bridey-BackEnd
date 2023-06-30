namespace BrideyApp.Models
{
    public class Composition:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductComposition> ProductCompositions { get; set; }
    }
}
