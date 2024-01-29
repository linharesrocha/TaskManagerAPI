using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.DTO
{
    public class AddTaskRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name Task has to be a minimum of 3 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
