using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class MaintenancePhaseDTO
    {
        public int? MaintenanceId { get; set; } = 0;

        public int? WebDevelopmentId { get; set; }

        [Required(ErrorMessage = "Maintenance title is required.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MaintenanceStatus Status { get; set; } = MaintenanceStatus.REPORTED;

        [Required(ErrorMessage = "The Issue Type field is required.")] 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IssueType IssueType { get; set; } = IssueType.GENERAL;

        [Required(ErrorMessage = "The Priority field is required.")] 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MaintenancePriority Priority { get; set; } = MaintenancePriority.LOW;

        public string? SystemAffected { get; set; }

        [Required(ErrorMessage = "The Start Time field is required.")] 
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "The End Time field is required.")] 
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; } = DateTime.UtcNow.AddDays(1);

        // Navigation properties
        [JsonIgnore]
        public WebDevelopment? WebDevelopment { get; set; } = null!;
    }
}