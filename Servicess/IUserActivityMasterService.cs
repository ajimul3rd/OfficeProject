using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IUserActivityMasterService
    {
        Task<List<UserActivityMasterDto>> GetAllUserActivityList();
        Task AddOrUpdateGetUserActivityList(UserActivityMasterDto activity);
        Task Delete(int Id);
    }
}
