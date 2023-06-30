namespace BrideyApp.Models
{
    public class ProductComposition : BaseEntity
    {
        public int ProductId { get; set; }
        public int CompositionId { get; set; }
        public Product Product { get; set; }
        public Composition Composition { get; set; }
    }
}
