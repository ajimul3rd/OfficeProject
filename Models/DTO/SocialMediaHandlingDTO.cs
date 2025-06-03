//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
//using OfficeProject.Models.Entities;
//using OfficeProject.Models.Enums;

//namespace OfficeProject.Models.DTO
//{
//    public class SocialMediaHandlingDTO
//    {
//        public int? SocialId { get; set; } = 0;

//        public int? MarketingTaskId { get; set; }

//        [Required(ErrorMessage = "Start time is required.")]
//        public DateTime StartTime { get; set; } = DateTime.UtcNow;

//        [Required(ErrorMessage = "Total posts are required.")]
//        public int TotalPosts { get; set; }

//        [Required(ErrorMessage = "Total followers are required.")]
//        public int TotalFollowers { get; set; }

//        [Required(ErrorMessage = "Total likes are required.")]
//        public int TotalLikes { get; set; }

//        [Required(ErrorMessage = "Engagement rate is required.")]
//        public double EngagementRate { get; set; }

//        [Required(ErrorMessage = "Issued by is required.")]
//        public string IssuedBy { get; set; }

//        [Required(ErrorMessage = "Progress status is required.")]
//        public string ProgressStatus { get; set; }

//        public string? Notes { get; set; }

//        // Navigation properties
//        [JsonIgnore]
//        public MarketingPhase? MarketingPhase { get; set; }
//    }
//}
