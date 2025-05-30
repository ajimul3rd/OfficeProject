using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Enums;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class MaintenancePhase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaintenanceId { get; set; }

        [Required]
        [ForeignKey(nameof(WebDevelopment))]
        public int WebDevelopmentId { get; set; }      

      
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; }

      
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MaintenanceStatus Status { get; set; } = MaintenanceStatus.REPORTED;

       
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IssueType IssueType { get; set; } = IssueType.GENERAL;
      
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MaintenancePriority Priority { get; set; } = MaintenancePriority.LOW;


        public string? SystemAffected { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; } 

        // Navigation properties
        [JsonIgnore]
        public WebDevelopment WebDevelopment { get; set; } = null!;
    
    }

   
}
