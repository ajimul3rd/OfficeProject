using System.ComponentModel.DataAnnotations;

namespace OfficeProject.Models.DTO
{
    public class UserActivityMasterDto
    {
        public int MasterId { get; set; }

        [Required(ErrorMessage = "Working activity is required")]
        public string MasterActivityName { get; set; }
       
    }
}
