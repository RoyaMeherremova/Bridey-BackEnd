using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.HomeBanner
{
    public class HomeBannerCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile SmallPhoto { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile LargePhoto { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
