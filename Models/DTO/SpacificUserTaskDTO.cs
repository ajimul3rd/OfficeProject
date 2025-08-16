using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class SpacificUserTaskDTO
    {
      
        public int? TaskId { get; set; }

        [Required]
        public int Service_Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? TaskDescription { get; set; }

        public string UserName { get; set; } = string.Empty;

        public bool IsSelected { get; set; }

    }
}
