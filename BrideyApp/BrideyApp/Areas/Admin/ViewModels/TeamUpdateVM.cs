using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class TeamUpdateVM
    {
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public string Name { get; set; }
        public int PositionId { get; set; }
    }
}
