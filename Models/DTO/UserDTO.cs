using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.DTO
{
    public class UserDTO
    {

            public int? UserId { get; set; } = 0;

            [Required(ErrorMessage = "Username is required")]
            [StringLength(50, ErrorMessage = "Username must be under 50 characters")]
            public string UserName { get; set; }
                  

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string UserEmail { get; set; }

            [Required(ErrorMessage = "Contact number is required")]
            [Phone(ErrorMessage = "Invalid phone number")]
            public string UserContact { get; set; }

            [Required(ErrorMessage = "User RoleMust be define")]
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public UserRole? Role { get; set; }
            public bool IsActive { get; set; } = true;

        public bool PreeAssignUserRole { get; set; } = false;

        [Required(ErrorMessage = "Company name must be under 100 characters")]
            public string? CompanyName { get; set; }

            [Required(ErrorMessage = "Address must be under 200 characters")]
            public string? Address { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            [Required(ErrorMessage = "Designation is Request")]
        public List<UserDesignation> UserDesignation { get; set; }

        [Required(ErrorMessage = "Invalid date format")]
            [DataType(DataType.Date)]
            public DateTime? JoiningDate { get; set; } = DateTime.UtcNow;

            //public List<Projects> Projects { get; set; }

        public List<ClientsDTO>? Clients { get; set; }
    }
}
