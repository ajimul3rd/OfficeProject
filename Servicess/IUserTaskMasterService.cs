using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IUserTaskMasterService
    {
        Task<List<UserTaskMaster>> GetUserTasksMasterAsync();
        Task AddOrUpdateUserTasksMasterAsync(UserTaskMaster task);
        Task DeleteTask(int taskId);
    }
}