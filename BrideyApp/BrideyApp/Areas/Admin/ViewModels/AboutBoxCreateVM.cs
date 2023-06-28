using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class AboutBoxCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
