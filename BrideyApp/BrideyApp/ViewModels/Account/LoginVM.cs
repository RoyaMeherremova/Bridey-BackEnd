using System.ComponentModel.DataAnnotations;

namespace BrideyApp.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
