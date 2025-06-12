using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Entities;

namespace OfficeProject.Models.DTO
{
    public class SeoTaskDetailsDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? SeoTaskId { get; set; }

        [Required(ErrorMessage = "Record Id Not Found")]
        public int WorkRecordId { get; set; }

        [Required(ErrorMessage = "KeyWord Not Found")]
        public string KeyWord { get; set; }

        [Required(ErrorMessage = "Current Rank is Requird")]
        public int CurrentRank { get; set; }

        public string? Note { get; set; }

        // Navigation property
        [JsonIgnore]
        public WorkingRecords? WorkRecords { get; set; } = null!;
    }
}
