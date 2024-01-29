using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Repositories.SQLServerImplementation;

namespace TaskManagerAPI.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        [HttpGet]
        public async IActionResult GetAll()
        {
            var tasksDomain = taskRepository.GetAllAsync();
        }
    }
}
