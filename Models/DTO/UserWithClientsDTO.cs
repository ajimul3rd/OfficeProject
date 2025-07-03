using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.DTO
{
    public class UserWithClientsDto
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserContact { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public List<UserDesignationDto>? UserDesignationDto { get; set; }
        public DateTime? JoiningDate { get; set; }
        public UserRole Role { get; set; } = UserRole.USER;

        public List<ClientsDTO> Clients { get; set; }
    }

}
