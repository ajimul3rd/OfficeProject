using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class UserDesignationDto
    {
        public int? DesignationId { get; set; }

        [Required(ErrorMessage = "User Designation required")]
        public string Designation { get; set; }
    }
}
