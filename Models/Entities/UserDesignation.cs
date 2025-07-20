using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class UserDesignation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationId { get; set; }

        [Required]
        [ForeignKey(nameof(Users))]
        public int User_Id { get; set; }
        public string Designation {  get; set; }

        [JsonIgnore]
        // Navigation property
        public Users? Users { get; set; }




    }
}
