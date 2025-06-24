using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Entities;

namespace OfficeProject.Models.DTO
{
    public class SeoTaskDetailsDto
    {
        public int? SeoTaskId { get; set; }

        [Required(ErrorMessage = "Reference Id is required")]
        public int WorkTaskId { get; set; }

        [Required(ErrorMessage = "Reference Key Word is required")]
        public string KeyWord { get; set; }

        [Required(ErrorMessage = "Current Rank value is Requird")]
        public int CurrentRank { get; set; }

        public string? Note { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;








 




    }
}
