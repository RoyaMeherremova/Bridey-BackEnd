namespace BrideyApp.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool SoftDelete { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
