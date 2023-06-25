using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class AdvertisingUpdateVM
    {
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

      
    }
}
