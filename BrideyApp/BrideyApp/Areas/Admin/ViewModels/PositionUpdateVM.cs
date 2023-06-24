using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class PositionUpdateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
