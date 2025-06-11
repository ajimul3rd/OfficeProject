using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class SeoTaskDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeoTaskId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkRecords))]
        public int WorkRecordId { get; set; }

        [Required]
        [MaxLength(150)]
        public string KeyWord { get; set; }

        [Required]
        public int CurrentRank { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        // Navigation property
        [JsonIgnore]
        public WorkingRecords? WorkRecords { get; set; } = null!;
    }
}
