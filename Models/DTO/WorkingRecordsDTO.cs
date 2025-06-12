using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Entities;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class WorkingRecordsDto
    {
        public int? WorkRecordId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; }

        public string ServiceName { get; set; }

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

        // Optionally include child DTOs if needed

        [JsonIgnore]
        public Services Services { get; set; } = null!;

        [ValidateComplexType]
        public List<SeoTaskDetailsDto>? SeoTaskDetailsDto { get; set; }

    }
}
