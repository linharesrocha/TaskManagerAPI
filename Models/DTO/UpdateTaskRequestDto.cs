using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.Models.Domain;

namespace TaskManagerAPI.Models.DTO
{
    public class UpdateTaskRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name Task has to be a minimum of 3 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }

        public PriorityLevel Priority { get; set; }
    }
}
