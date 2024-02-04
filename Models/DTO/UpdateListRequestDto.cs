using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.DTO
{
    public class UpdateListRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
