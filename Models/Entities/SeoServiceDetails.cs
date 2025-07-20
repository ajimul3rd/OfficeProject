using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class SeoServiceDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeoServiceDetailsId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(150)]
        public string KeyWord { get; set; }

        public int Rank { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [MaxLength(250)]
        public string? ExtraField1 { get; set; }

        [MaxLength(250)]
        public string? ExtraField2 { get; set; }

        // Navigation property
        [JsonIgnore]
        public Services Services { get; set; } = null!;
    }
}
