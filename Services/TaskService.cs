using AspAplication.Data;
using AspAplication.Dtos;
using AspAplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AspAplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<TaskItem> CreateAsync(CreateTaskRequest request)
        {
            TaskItem task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskRequest request)
        {
            TaskItem? task = await GetByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            task.Title = request.Title;
            task.Description = request.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CompleteAsync(int id)
        {
            TaskItem? task = await GetByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            task.IsCompleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TaskItem? task = await GetByIdAsync(id);

            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}