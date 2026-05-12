using AspAplication.Models;
using AspAplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspAplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            TaskItem? task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            TaskItem createdTask = _taskService.Create(task);

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem task)
        {
            bool updated = _taskService.Update(id, task);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            bool completed = _taskService.Complete(id);

            if (!completed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _taskService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}