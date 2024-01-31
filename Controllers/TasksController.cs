using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.CustomActionFilters;
using TaskManagerAPI.Models.Domain;
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
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var tasksDomain = await taskRepository.GetAllAsync(
                filterOn, filterQuery, 
                sortBy, isAscending ?? true,
                pageNumber, pageSize);

            var tasksDto = mapper.Map<List<TaskDto>>(tasksDomain);

            return Ok(tasksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var taskDomain = await taskRepository.GetByIdAsync(id);
            
            if (taskDomain == null)
            {
                return NotFound();
            }

            var taskDto = mapper.Map<TaskDto>(taskDomain);

            return Ok(taskDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddTaskRequestDto addTaskRequestDto)
        {
            
            var taskDomain = mapper.Map<Models.Domain.Task>(addTaskRequestDto);

            taskDomain = await taskRepository.CreateAsync(taskDomain);

            var taskDto = mapper.Map<TaskDto>(taskDomain);

            return CreatedAtAction(nameof(GetById), new { id = taskDto.Id}, taskDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var taskDomain = await taskRepository.DeleteAsync(id);

            if (taskDomain == null)
            {
                return NotFound();
            }

            var taskDto = mapper.Map<TaskDto>(taskDomain);

            return Ok(taskDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskRequestDto updateTaskRequestDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var taskDomain = mapper.Map<Models.Domain.Task>(updateTaskRequestDto);

            taskDomain = await taskRepository.UpdateAsync(id, taskDomain);

            if (taskDomain == null)
            {
                return NotFound();
            }

            var taskDto = mapper.Map<TaskDto>(taskDomain);

            return Ok(taskDto);
        }
    }
}
