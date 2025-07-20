using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.DTO
{
    public class UserInfoDTO
    {   public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole? Role { get; set; }
    }
}
