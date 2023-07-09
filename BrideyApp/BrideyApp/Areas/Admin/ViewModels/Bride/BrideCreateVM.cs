using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Bride
{
    public class BrideCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
    }
}
