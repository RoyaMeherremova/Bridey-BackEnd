using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Color
{
    public class ColorUpdateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
