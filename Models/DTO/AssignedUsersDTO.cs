using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class AssignedUsersDTO
    {
        
        public int? AssignedUsersId { get; set; }

        public int? ProjectId { get; set; }
 
        public int? UserId { get; set; }
       
        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        // Navigation properties
        [JsonIgnore]
        public Projects? Project { get; set; } = null!;

        [JsonIgnore]
        public Users? User { get; set; } = null!;
    }
}
