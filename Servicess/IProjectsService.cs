using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
namespace OfficeProject.Servicess
{
    public interface IProjectsService
    {
        Task AddProjectsAsync(ProjectsDTO project);
        Task<ProjectsDTO?> GetProjectByIdAsync(int projectId );
        Task <List<ProjectsDTO?>> GetProjectPerUserAsync();

        Task<List<ProjectsDTO?>> GetWorkingRecordPerUserAsync();
        Task<List<ProjectsDTO?>> GetAllProjectAsync();
        Task<ProjectsDTO?> GetProjectDTOById(int id);
        Task UpdateProjectAsync(ProjectsDTO project);
        Task SaveOrUpdateProjectsAsync(ProjectsDTO projectsDTO);

    }
}
