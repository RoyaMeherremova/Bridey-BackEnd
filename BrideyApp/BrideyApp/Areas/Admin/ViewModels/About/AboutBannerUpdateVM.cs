using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.About
{
    public class AboutBannerUpdateVM
    {
        public IFormFile SmallPhoto { get; set; }
        public IFormFile LargePhoto { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
    }
}
