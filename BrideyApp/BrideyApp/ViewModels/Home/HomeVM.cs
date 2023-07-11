using BrideyApp.Models;

namespace BrideyApp.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public HomeBanner HomeBanner { get; set; }
        public AboutUs AboutUs { get; set; }
        public List<Bride> Brides { get; set; }
        public List<Team> Teams { get; set; }
        public List<Advertising> Advertisings { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public List<Models.Blog> Blogs { get; set; }
        public List<Models.Product> Products { get; set; }
        public string Email { get; set; }


    }
}
