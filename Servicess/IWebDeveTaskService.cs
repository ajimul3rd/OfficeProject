using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IWebDeveTaskService
    {
        Task<bool> SaveOrUpdateWebTaskAsync(WebDeveTaskDetailsDto data);
    }
}
