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

        [ForeignKey(nameof(WorkRecords))]
        public int WorkTaskId { get; set; }

        [MaxLength(150)]
        public string KeyWord { get; set; }   
        
        public int CurrentRank { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;
    }
}
