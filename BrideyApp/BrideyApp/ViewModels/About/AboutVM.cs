using BrideyApp.Models;

namespace BrideyApp.ViewModels.About
{
    public class AboutVM
    {
        public AboutBanner AboutBanner { get; set; }
        public Dictionary<string, string> SectionBackgroundImages { get; set; }
        public Dictionary<string, string> HeaderBackgrounds { get; set; }
        public List<AboutBox> AboutBoxes { get; set; }
        public List<Team> Teams { get; set; }
        public List<Advertising> Advertisings { get; set; }

    }
}
