namespace BrideyApp.Models
{
    public class HomeBanner:BaseEntity
    {
        public string LargeImage { get; set; }

        public string SmallImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
