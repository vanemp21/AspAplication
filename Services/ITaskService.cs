using AspAplication.Dtos;
using AspAplication.Models;

namespace AspAplication.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();

        TaskItem? GetById(int id);

        TaskItem Create(CreateTaskRequest request);

        bool Update(int id, UpdateTaskRequest request);

        bool Complete(int id);

        bool Delete(int id);
    }
}