using BrideyApp.Models;

namespace BrideyApp.ViewModels
{
    public class BlogVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
        public List<Composition> Compositions { get; set; }
        public List<Advertising> Advertisings { get; set; }


    }
}
