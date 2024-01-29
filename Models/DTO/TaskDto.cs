namespace TaskManagerAPI.Models.DTO
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
