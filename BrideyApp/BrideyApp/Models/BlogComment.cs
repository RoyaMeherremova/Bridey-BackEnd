namespace BrideyApp.Models
{
    public class BlogComment : BaseEntity
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
