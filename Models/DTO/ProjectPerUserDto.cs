using OfficeProject.Models.Entities;

namespace OfficeProject.Models.DTO
{
    public class ProjectPerUserDto
    {
        public List<Projects>? Projects { get; set; }
        public Users? Users { get; set; }

    }
}
