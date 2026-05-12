using AspAplication.Dtos;
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
        public ActionResult<List<TaskResponse>> GetAll()
        {
            List<TaskResponse> tasks = _taskService.GetAll()
                .Select(task => ToResponse(task))
                .ToList();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskResponse> GetById(int id)
        {
            TaskItem? task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(task));
        }

        [HttpPost]
        public ActionResult<TaskResponse> Create(CreateTaskRequest request)
        {
            TaskItem createdTask = _taskService.Create(request);

            TaskResponse response = ToResponse(createdTask);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTaskRequest request)
        {
            bool updated = _taskService.Update(id, request);

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

        private static TaskResponse ToResponse(TaskItem task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt
            };
        }
    }
}