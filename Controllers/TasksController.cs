using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models.DTO;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Repositories.SQLServerImplementation;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;

        public TasksController(ITaskRepository taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasksDomain = await taskRepository.GetAllAsync();

            var tasksDto = mapper.Map<List<TaskDto>>(tasksDomain);

            return Ok(tasksDto);
        }
    }
}
