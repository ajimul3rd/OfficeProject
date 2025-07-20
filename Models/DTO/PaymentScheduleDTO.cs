using OfficeProject.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class PaymentScheduleDTO
    {
        public int? ScheduleId { get; set; }


        public int? ProjectId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }

        // Navigation property
        [JsonIgnore]
        public ServicesDTO Services { get; set; } = null!;
    }
}
