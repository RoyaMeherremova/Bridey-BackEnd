using BrideyApp.Models;

namespace BrideyApp.ViewModels
{
    public class ContactVM
    {
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<Advertising> Advertisings { get; set; }


    }
}
