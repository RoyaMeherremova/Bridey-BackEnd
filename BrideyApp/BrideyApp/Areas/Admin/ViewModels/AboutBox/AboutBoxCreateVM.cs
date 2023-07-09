using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.AboutBox
{
    public class AboutBoxCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
