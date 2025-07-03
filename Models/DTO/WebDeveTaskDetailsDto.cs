using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class WebDeveTaskDetailsDto
    {
       
        public int? webDevTaskId { get; set; }

        [Required(ErrorMessage = "Reference Id is required")]
        public int WorkTaskId { get; set; }

        [Required(ErrorMessage = "User working task is required")]
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
