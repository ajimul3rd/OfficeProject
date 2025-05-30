using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class DesignPhaseDTO
    {
        public int? DesignTaskId { get; set; } = 0;

        public int? WebDevelopmentId { get; set; }

        [Required(ErrorMessage = "Phase title is required.")]
        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DesignStatus Status { get; set; } = DesignStatus.ASSIGNED;

        public string? DesignTool { get; set; }

        [Url(ErrorMessage = "The MockupLink must be a valid URL.")]
        [StringLength(200, ErrorMessage = "The MockupLink cannot exceed 200 characters.")]
        public string? MockupLink { get; set; }

        [Required(ErrorMessage = "The FeedbackStatus field is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FeedbackStatus FeedbackStatus { get; set; } = FeedbackStatus.PENDING;

        [Required(ErrorMessage = "The StartTime field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "The EndTime field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "The DeadlineDate field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? DeadlineDate { get; set; }

        //Navigation Property
        [JsonIgnore]
        public WebDevelopment? WebDevelopment { get; set; } = null!;
    }
}