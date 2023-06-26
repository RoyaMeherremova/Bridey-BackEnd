using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class SocialCreateVM
    {
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
