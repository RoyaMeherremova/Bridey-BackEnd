using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class AboutUsUpdateVM
    {
        public IFormFile SmallPhoto { get; set; }

        public IFormFile LargePhoto { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    }
}
