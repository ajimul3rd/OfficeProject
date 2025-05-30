
namespace OfficeProject.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Clients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Required] 
        [ForeignKey(nameof(User))] 
        public int UserId { get; set; }
       
        public string ClientName { get; set; }

        public string CompanyName { get; set; }

        public  string? ClientContact1 { get; set; }       

        public string? ClientContact2 { get; set; }

        public  string? ClientEmail1 { get; set; }
        
        public string? ClientEmail2 { get; set; }

        public string? ClientAddress { get; set; }

        public string? ClientCity { get; set; }

        public string? ClientSource { get; set; }

        public string? ClientSourceNote { get; set; }        

        public bool IsActive { get; set; } = true; 

        [DataType(DataType.DateTime)]
        public DateTime? IssueDate { get; set; }= DateTime.UtcNow;

        public string? IssuedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ClientCreatedAt { get; set; } = DateTime.UtcNow;

        public List<Projects>? Projects { get; set; }

        public Accounts? Accounts { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Users User { get; set; } = null!;
       
       
    }

}

