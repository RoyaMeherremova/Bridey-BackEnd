namespace BrideyApp.Models
{
    public class Team:BaseEntity
    {
        public string Image { get; set; }   
        public string Name { get; set; }
        public int PositionId { get; set; } 
        public Position Position { get; set; }  
        public string Testimonials { get; set; } 
    }
}
