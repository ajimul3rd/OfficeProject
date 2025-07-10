using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class PaymentSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; } = null;

        // Navigation property
        [JsonIgnore]
        public Services Services { get; set; } = null!;
    }
}
