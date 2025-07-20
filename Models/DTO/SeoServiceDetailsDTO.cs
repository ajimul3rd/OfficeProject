using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class SeoServiceDetailsDTO
    {
        public int? SeoServiceDetailsId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Keyword is required")]
        [MaxLength(150)]
        public string KeyWord { get; set; }

        public int Rank { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }
    }
}
