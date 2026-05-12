using AspAplication.Dtos;
using AspAplication.Models;

namespace AspAplication.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetByIdAsync(int id);

        Task<TaskItem> CreateAsync(CreateTaskRequest request);

        Task<bool> UpdateAsync(int id, UpdateTaskRequest request);

        Task<bool> CompleteAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}