using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{

public class AssignedUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignedUsersId { get; set; }

        [Required]
        [ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }

        [Required]
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public Projects? Projects { get; set; } = null!;

        // Navigation properties
        [JsonIgnore]
        public Users? Users { get; set; } = null!;
    }
}
