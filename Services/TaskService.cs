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

        public TaskItem Create(TaskItem task)
        {
            task.Id = _nextId;
            task.CreatedAt = DateTime.Now;
            task.IsCompleted = false;

            _tasks.Add(task);

            _nextId++;

            return task;
        }

        public bool Update(int id, TaskItem updatedTask)
        {
            TaskItem? task = GetById(id);

            if (task == null)
            {
                return false;
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;

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