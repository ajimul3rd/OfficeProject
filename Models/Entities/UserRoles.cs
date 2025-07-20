using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace OfficeProject.Models.Entities
{
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public string Role { get; set; }

        // Navigation property
        [ForeignKey(nameof(UserId))]
        [Required]
        public int UserId { get; set; }
        public Users? Users { get; set; }
    }
}
