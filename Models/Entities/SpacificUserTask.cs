using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class SpacificUserTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        [ForeignKey(nameof(Services))]
        public int Service_Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? TaskDescription { get; set; }
       
        [JsonIgnore]
        public Services Services { get; set; } = null!;
    }
}
