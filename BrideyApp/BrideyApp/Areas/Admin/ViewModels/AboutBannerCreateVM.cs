using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class AboutBannerCreateVM
    {
        [Required]
        public IFormFile SmallPhoto { get; set; }
        [Required]
        public IFormFile LargePhoto { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
