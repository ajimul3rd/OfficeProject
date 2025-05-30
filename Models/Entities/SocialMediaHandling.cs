using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OfficeProject.Models.Enums;

namespace OfficeProject.Models.Entities
{
    public class SocialMediaHandling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SocialId { get; set; }

        [Required]
        [ForeignKey(nameof(MarketingPhase))]
        public int MarketingTaskId { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        public int? TotalPosts { get; set; }

        public int? TotalFollowers { get; set; }

        public int? TotalLikes { get; set; }

        public double? EngagementRate { get; set; }

        public string? IssuedBy { get; set; }

        public string? ProgressStatus { get; set; }

        public string? Notes { get; set; }

        // Navigation properties
        [JsonIgnore]
        public MarketingPhase? MarketingPhase { get; set; }
    }
}
