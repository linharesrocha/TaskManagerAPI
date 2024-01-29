﻿using AutoMapper;
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
    }
}
