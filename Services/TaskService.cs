using AspAplication.Dtos;
using AspAplication.Models;

namespace AspAplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new();

        private int _nextId = 1;

        public List<TaskItem> GetAll()
        {
            return _tasks;
        }

        public TaskItem? GetById(int id)
        {
            return _tasks.FirstOrDefault(task => task.Id == id);
        }

        public TaskItem Create(CreateTaskRequest request)
        {
            TaskItem task = new TaskItem
            {
                Id = _nextId,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _tasks.Add(task);

            _nextId++;

            return task;
        }

        public bool Update(int id, UpdateTaskRequest request)
        {
            TaskItem? task = GetById(id);

            if (task == null)
            {
                return false;
            }

            task.Title = request.Title;
            task.Description = request.Description;

            return true;
        }

        public bool Complete(int id)
        {
            TaskItem? task = GetById(id);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;

            return true;
        }

        public bool Delete(int id)
        {
            TaskItem? task = GetById(id);

            if (task == null)
            {
                return false;
            }

            _tasks.Remove(task);

            return true;
        }
    }
}