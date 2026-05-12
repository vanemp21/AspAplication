using AspAplication.Data;
using AspAplication.Dtos;
using AspAplication.Models;

namespace AspAplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _context.Tasks.FirstOrDefault(task => task.Id == id);
        }

        public TaskItem Create(CreateTaskRequest request)
        {
            TaskItem task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

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

            _context.SaveChanges();

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

            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            TaskItem? task = GetById(id);

            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return true;
        }
    }
}