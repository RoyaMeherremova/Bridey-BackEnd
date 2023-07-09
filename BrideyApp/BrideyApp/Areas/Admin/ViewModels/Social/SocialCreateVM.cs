using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Social
{
    public class SocialCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Icon { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Link { get; set; }
    }
}
