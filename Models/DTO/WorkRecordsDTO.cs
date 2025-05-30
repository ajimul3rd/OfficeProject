using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class WorkRecordsDTO
    {
        public int? WorkRecordId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime WorkDate { get; set; }

        public int Post { get; set; }

        public int Reels { get; set; }

        public int Ads { get; set; }

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
        public List<WorkRecordsSeoDetailsDTO>? WorkRecordsSeoDetails { get; set; }
    }
}
