using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.DTO;

namespace OfficeProject.Models.Entities
{
    public class WorkTaskDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkRecordId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }

        [Required]
        [ForeignKey(nameof(Users))]
        public int Work_UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; }

        //public string ServiceName { get; set; }

        public int SharedPost { get; set; }

        public int CreatedReels { get; set; }

        public int UsedAdsBudget { get; set; }

        public string Task { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }

        [MaxLength(250)]
        public string? ExtraField3 { get; set; }

        [MaxLength(250)]
        public string? ExtraField4 { get; set; }

        [MaxLength(250)]
        public string? ExtraField5 { get; set; }

        [MaxLength(250)]
        public string? ExtraField6 { get; set; }

        [MaxLength(250)]
        public string? ExtraField7 { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Services Services { get; set; } = null!;

        [JsonIgnore]
        public Users Users { get; set; } = null!;

        public List<SeoTaskDetails>? SeoTaskDetails { get; set; }
        public List<OthersTaskDetails>? OthersTaskDetails { get; set; }
        public List<WebDeveTaskDetails>? WebDeveTaskDetails { get; set; }

    }
}
