namespace BrideyApp.Models
{
    public class WishlistProduct:BaseEntity
    {
        public int Count { get; set; }
        public int ProductId { get; set; }
        public int WishlistId { get; set; }
        public Product Product { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
