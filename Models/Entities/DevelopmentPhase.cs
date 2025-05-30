using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Enums;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class DevelopmentPhase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DevelopmentTaskId { get; set; }

        [Required]
        [ForeignKey(nameof(WebDevelopment))]
        public int WebDevelopmentId { get; set; }

       
        public string? Title { get; set; }

       
        public string? Description { get; set; }

       
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DevelopmentStatus Status { get; set; } = DevelopmentStatus.ASSIGNED;

       
        public string? ProgrammingLanguage { get; set; }

        
        public string? CodeRepoUrl { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestingStatus TestStatus { get; set; } = TestingStatus.NOT_TESTED;


        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeadlineDate { get; set; }

        // Navigation properties
        [JsonIgnore]
        public WebDevelopment WebDevelopment { get; set; } = null!;
  
    }

}
