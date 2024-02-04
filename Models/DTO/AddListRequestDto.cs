using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.DTO
{
    public class AddListRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
