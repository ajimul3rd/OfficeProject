using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class BacklinkDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BacklinkId { get; set; }
        public int BacklinkUrlId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkRecords))]
        public int WorkTaskId { get; set; }
        public string SiteUrl { get; set; }
        public string PublishUrl { get; set; }
        public TimeSpan? PublishTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;
    }
}
