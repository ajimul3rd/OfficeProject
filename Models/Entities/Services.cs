using OfficeProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class Services
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        [ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }

        [Required]
        [ForeignKey(nameof(Products))]
        public int ProductsId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "nvarchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BillingType? BillingType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int TotalPost { get; set; }

        public int TotalReels { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AdsBudget { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }

        [MaxLength(250)]
        public string? ExtraField3 { get; set; }
        [JsonIgnore]
        public Products Products { get; set; } = null!;
        public List<SeoServiceDetails>? SeoServiceDetails { get; set; }
        public List<OthersService>? OthersServices { get; set; }
        public List<WorkRecords>? WorkRecords { get; set; }
        public List<WebDevelopment>? WebDevelopment { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Projects Projects { get; set; } = null!;


    }
}
