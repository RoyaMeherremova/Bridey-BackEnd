using BrideyApp.Helpers;
using BrideyApp.Models;

namespace BrideyApp.ViewModels.Blog
{
    public class BlogVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public Models.Blog Blog { get; set; }
        public List<Models.Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
        public List<Composition> Compositions { get; set; }
        public List<Advertising> Advertisings { get; set; }
        public Paginate<Models.Blog> PaginatedDatas { get; set; }
        public BlogCommentVM BlogCommentVM { get; set; }
        public List<Models.Blog> RelatedBlogs { get; set; }


    }
}
