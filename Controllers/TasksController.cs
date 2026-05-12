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
        public async Task<ActionResult<List<TaskResponse>>> GetAll()
        {
            List<TaskResponse> tasks = (await _taskService.GetAllAsync())
                .Select(task => ToResponse(task))
                .ToList();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponse>> GetById(int id)
        {
            TaskItem? task = await _taskService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(ToResponse(task));
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponse>> Create(CreateTaskRequest request)
        {
            TaskItem createdTask = await _taskService.CreateAsync(request);

            TaskResponse response = ToResponse(createdTask);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskRequest request)
        {
            bool updated = await _taskService.UpdateAsync(id, request);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            bool completed = await _taskService.CompleteAsync(id);

            if (!completed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _taskService.DeleteAsync(id);

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