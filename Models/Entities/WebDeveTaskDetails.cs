using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class WebDeveTaskDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int webDevTaskId { get; set; }

        [Required]
        [ForeignKey(nameof(WorkRecords))]
        public int WorkTaskId { get; set; }

        public string Task { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }

        public string? Note { get; set; }

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;
    }
}
