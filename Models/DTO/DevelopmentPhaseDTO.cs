using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class DevelopmentPhaseDTO
    {
        public int? DevelopmentTaskId { get; set; } = 0;
        public int? WebDevelopmentId { get; set; }

        [Required(ErrorMessage = "Phase title is required.")]
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DevelopmentStatus Status { get; set; } = DevelopmentStatus.ASSIGNED;

        [Required(ErrorMessage = "Programming Language is required.")]        
        public string? ProgrammingLanguage { get; set; }

        public string? CodeRepoUrl { get; set; }

        [Required(ErrorMessage = "Testing Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestingStatus TestStatus { get; set; } = TestingStatus.NOT_TESTED;

        [Required(ErrorMessage = "The Start Time field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "The End Time field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "The Deadline Date field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? DeadlineDate { get; set; }

        // Navigation Property
        [JsonIgnore]
        public WebDevelopment? WebDevelopment { get; set; } = null!;
    }
}
