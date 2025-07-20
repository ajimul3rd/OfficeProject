using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class OthersServiceDTO
    {
        public int? OthersId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Label Name is required")]
        [MaxLength(150)]
        public string LableName { get; set; }

        public int Post { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }
    }
}
