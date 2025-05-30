using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IUserRolesService
    {
        Task<List<UserRoles>> GetAllUserRolesAsync();
        Task<string> AddOrUpdateUserRoleAsync(UserRoles userRole);
    }
}
