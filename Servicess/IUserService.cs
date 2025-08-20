using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
namespace OfficeProject.Servicess
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersDTOAsync();
        Task<List<Users>> GetAllUsersAsync();
        Task<UserDTO?> GetUserDTOById(int id);
        Task<Users?> GetUserById(int id);
        Task<UserDTO?> GetUserDTOByUsername(string username);
        Task<Users> GetUserUserDesignationAsync();
        Task<Users?> GetUserByUsername(string username);
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(UserDTO user);
        Task UpdateRefreshToken(int userId, string refreshToken);
        Task RevokeRefreshToken(string username);
        Task<UserDTO?> GetAllUsersDTOClientAsync(UserWithClientsDto userWithClientsDto);//old

        Task<UserDTO?> GetCurrentLoggedUserAsync();
        Task<List<UserDTO>> GetPreeAssignUsersAsync();

    }
}
