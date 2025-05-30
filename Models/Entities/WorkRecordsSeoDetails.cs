using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class WorkRecordsSeoDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkRecordsSeoId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkRecords))]
        public int WorkRecordId { get; set; }

        [Required]
        [MaxLength(150)]
        public string KeyWord { get; set; }

        [Required]
        public int Rank { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        // Navigation property
        [JsonIgnore]
        public WorkRecords? WorkRecords { get; set; } = null!;
    }
}
