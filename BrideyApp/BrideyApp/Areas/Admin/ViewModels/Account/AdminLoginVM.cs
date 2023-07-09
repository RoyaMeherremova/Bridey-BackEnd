using System.ComponentModel.DataAnnotations;

namespace BrideyApp.Areas.Admin.ViewModels.Account
{
    public class AdminLoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
