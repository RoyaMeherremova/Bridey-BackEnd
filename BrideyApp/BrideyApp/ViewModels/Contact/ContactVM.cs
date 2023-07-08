using BrideyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BrideyApp.ViewModels.Contact
{
    public class ContactVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<Advertising> Advertisings { get; set; }


        [Required(ErrorMessage = "Don`t be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }


    }
}
