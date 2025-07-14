using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetTask();
        Task AddOrUpdateTask(UserTask task);
        Task DeleteTask(int taskId);
    }
}