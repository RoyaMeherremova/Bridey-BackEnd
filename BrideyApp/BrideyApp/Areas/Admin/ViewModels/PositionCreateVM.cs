using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels
{
    public class PositionCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
