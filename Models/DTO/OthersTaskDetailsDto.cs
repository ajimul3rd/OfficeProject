using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class OthersTaskDetailsDto
    {

        public int? OthersTaskId { get; set; }

        [Required(ErrorMessage = "Reference Id is required")]
        public int WorkTaskId { get; set; }

        [Required(ErrorMessage = " Lable name is required")]
        public string LableName { get; set; }

        [Required(ErrorMessage = "Shared post value is required")]
        public int SharedPost { get; set; }

        public string? Note { get; set; }

        [DataType(DataType.Date)]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        // Navigation property
        [JsonIgnore]
        public WorkTaskDetails? WorkRecords { get; set; } = null!;



       

    }
}
