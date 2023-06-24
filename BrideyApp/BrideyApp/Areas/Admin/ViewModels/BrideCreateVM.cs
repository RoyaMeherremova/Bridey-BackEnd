using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class BrideCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
