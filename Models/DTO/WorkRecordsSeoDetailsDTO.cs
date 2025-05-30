using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Entities;

namespace OfficeProject.Models.DTO
{
    public class WorkRecordsSeoDetailsDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkRecordsSeoId { get; set; }

        [Required]
        public int WorkRecordId { get; set; }

        [Required]
        public string KeyWord { get; set; }

        [Required]
        public int Rank { get; set; }

        public string? Note { get; set; }

        // Navigation property
        [JsonIgnore]
        public WorkRecords? WorkRecords { get; set; } = null!;
    }
}
