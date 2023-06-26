using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class SocialUpdateVM
    {
        [Required]
        public string Icon { get; set; }

        [Required]
        public string Link { get; set; }
    }
}
