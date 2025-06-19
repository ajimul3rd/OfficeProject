using System.ComponentModel.DataAnnotations;
using OfficeProject.Models.Entities;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.DTO
{
    public class WorkTaskDetailsDto
    {
        public int? WorkRecordId { get; set; }

        [Required(ErrorMessage ="Service id is Required")]
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public int Work_UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]

        public DateTime WorkDate { get; set; } = DateTime.Today;

        //public string ServiceName { get; set; }

        public int SharedPost { get; set; } = 0;

        public int CreatedReels { get; set; } = 0;

        public int UsedAdsBudget { get; set; } = 0;

        [Required(ErrorMessage = "User Task is required")]
        public string Task { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Working Status is required")]
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
        public Services? Services { get; set; } = null!;

        [JsonIgnore]
        public Users? Users { get; set; } = null!;

        [ValidateComplexType]
        public List<SeoTaskDetailsDto>? SeoTaskDetailsDto { get; set; }

        [ValidateComplexType]
        public List<OthersTaskDetailsDto>? OthersTaskDetailsDto { get; set; }

        [ValidateComplexType]
        public List<WebDeveTaskDetailsDto>? WebDeveTaskDetailsDto { get; set; }

    }
}
