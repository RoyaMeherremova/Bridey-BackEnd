using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class SliderUpdateVM
    {
        public IFormFile Photo { get; set; }
        [Required]
        public string Header { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDesc { get; set; }
        public string Image { get; set; }

    }
}
