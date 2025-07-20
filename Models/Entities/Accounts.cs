using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OfficeProject.Models.Entities
{
    public class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal OpeningBalance { get; set; } = 0;

        public bool IsClosed { get; set; } = false;

        [Required]
        [Precision(18, 2)]
        public decimal ClosedBalance { get; set; } = 0;

        [Required]
        [DataType(DataType.Date)]
        public DateTime ClosedDate { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal TotalPaymentsMade { get; set; } = 0;

        public List<Transactions>? Transactions { get; set; }

        // Navigation property
        [JsonIgnore]
        public Clients Client { get; set; } = null!;
        
    }
}
