using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class OthersService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OthersId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(150)]
        public string LableName { get; set; }

        public int Post { get; set; }

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
