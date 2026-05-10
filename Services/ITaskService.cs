using AspAplication.Models;

namespace AspAplication.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAll();

        TaskItem? GetById(int id);

        TaskItem Create(TaskItem task);

        bool Complete(int id);

        bool Delete(int id);
    }
}