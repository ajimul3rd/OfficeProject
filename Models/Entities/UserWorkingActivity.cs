using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OfficeProject.Models.Entities
{
    public class UserWorkingActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int workingActivityId { get; set; }

        [ForeignKey("ProductsId")]
        public int ProductsId { get; set; }

        public string WorkingActivityName {  get; set; }

        [JsonIgnore]
        public Products Products { get; set; }

    }
}
