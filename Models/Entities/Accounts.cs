using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public decimal Balance { get; set; } = 0;

        [Required]
        public decimal TotalPaymentsMade { get; set; } = 0;

        public List<Transactions>? Transactions { get; set; }

        // Navigation property
        [JsonIgnore]
        public Clients Client { get; set; } = null!;

        
    }
}
