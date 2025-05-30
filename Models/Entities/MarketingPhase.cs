using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Enums;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class MarketingPhase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarketingTaskId { get; set; }

        [Required]
        [ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }

       public string? WebsiteUrl { get; set; }

        public string? Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MarketingTypes MarketingTypes { get; set; } = MarketingTypes.SOCIAL_MEDIA_HANDLING;
        public string? Description { get; set; }
       
        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MarketingStatus Status { get; set; } = MarketingStatus.PLANNED;

        public decimal Budget { get; set; }
              
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [JsonIgnore]
        public Projects Projects { get; set; } = null!;

        public SocialMediaHandling? SocialMediaHandling { get; set; }

        public Seo? Seo { get; set; }
    }

}
