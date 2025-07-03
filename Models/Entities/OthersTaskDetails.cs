using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class OthersTaskDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OthersTaskId { get; set; }


        [ForeignKey(nameof(WorkRecords))]
        public int WorkTaskId { get; set; }

  
        [MaxLength(150)]
        public string LableName { get; set; }

        public int SharedPost { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;
    }
}
