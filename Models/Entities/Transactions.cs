using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OfficeProject.Models.Entities
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        [ForeignKey(nameof(Accounts))]
        public int AccountId { get; set; }

        [ForeignKey(nameof(PaymentSchedule))]
        public int? ScheduleId { get; set; }

        [Precision(18, 2)]
        public decimal AmountPaid { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        // Navigation properties
        [JsonIgnore]
        public Accounts Accounts { get; set; } = null!;

        // Navigation properties
        [JsonIgnore]
        public PaymentSchedule? PaymentSchedule { get; set; }
    }
}
