using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IGlobalDataService
    {
        List<ProjectsDTO> Projects { get; set; }
        ProjectsDTO Data { get; set; }
        bool HasData { get; }
        void Clear();

    }
}
