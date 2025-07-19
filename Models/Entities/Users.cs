using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserContact { get; set; }

        public string UserPassword { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole? Role { get; set; }

        public bool IsActive { get; set; } = true;

        public bool PreeAssignUserRole { get; set; } = false;

        public string? RefreshToken { get; set; } // "?" makes it nullable

        public DateTime? RefreshTokenExpiry { get; set; }

        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public List<UserDesignation>? UserDesignation { get; set; }

        public DateTime? JoiningDate { get; set; }

        // Navigation

        public ICollection<UserRoles>? UserRoles { get; set; }

        public List<Clients>? Clients { get; set; }



    }

}
