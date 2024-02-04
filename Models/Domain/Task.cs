namespace TaskManagerAPI.Models.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public PriorityLevel? Priority { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ListId { get; set; }
        public List List { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
