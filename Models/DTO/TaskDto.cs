using TaskManagerAPI.Models.Domain;

namespace TaskManagerAPI.Models.DTO
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PriorityLevel? Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
