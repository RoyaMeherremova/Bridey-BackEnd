using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class AdvertisingCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
       
    }
}
