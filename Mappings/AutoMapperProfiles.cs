using AutoMapper;
using TaskManagerAPI.Models.DTO;

namespace TaskManagerAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.Task, TaskDto>().ReverseMap();
            CreateMap<Models.Domain.Task, AddTaskRequestDto>().ReverseMap();
            CreateMap<Models.Domain.Task, UpdateTaskRequestDto>().ReverseMap();
        }
    }
}
