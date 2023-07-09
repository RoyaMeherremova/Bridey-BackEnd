using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Size
{
    public class SizeCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
