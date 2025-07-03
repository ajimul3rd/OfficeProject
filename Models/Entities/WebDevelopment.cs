using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class WebDevelopment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebDevelopmentId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }

        public string? DomainName { get; set; }

        public DateTime? HostingDate { get; set; }

        public DateTime? HostingRenewalDate { get; set; }

        public string? HostingLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? HostingRenewalAmount { get; set; }

        public string? ServerFtpAssign { get; set; }

        public string? ServerIp { get; set; }

        public string? ServerUserId { get; set; }

        public string? ServerPassword { get; set; }

        public string? DesignTools { get; set; }

        public string? MackupLink { get; set; }

        public string? Languages { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

        public string? Remarks { get; set; }

        public string? Note { get; set; }

        [JsonIgnore]
        public Services Services { get; set; } = null!;
    }
}
