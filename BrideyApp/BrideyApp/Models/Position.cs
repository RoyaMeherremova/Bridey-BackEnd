using System.Reflection.Metadata;

namespace BrideyApp.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }

    }
}
