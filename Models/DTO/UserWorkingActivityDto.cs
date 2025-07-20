using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OfficeProject.Models.Entities;

namespace OfficeProject.Models.DTO
{
    public class UserWorkingActivityDto
    {
        public int? WorkingActivityId { get; set; }
        public int ProductsId { get; set; }

        [Required(ErrorMessage = "Working activity is required")]
        public string WorkingActivityName { get; set; }

        [JsonIgnore]
        public Products? products { get; set; }

      

    }
}

