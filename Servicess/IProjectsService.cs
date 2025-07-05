using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
namespace OfficeProject.Servicess
{
    public interface IProjectsService
    {
        Task AddProjectsAsync(ProjectsDTO Project);
        Task<ProjectsDTO?> GetProjectByIdAsync(int ProjectId );
        Task<List<ProjectsDTO?>> GetTeamWorksAsync(int UserId); 
        Task <List<ProjectsDTO?>> GetProjectPerUserAsync();
        Task<List<ProjectsDTO?>> GetAllProjectAsync();
        Task<ProjectsDTO?> GetProjectDTOById(int Id);
        Task UpdateProjectAsync(ProjectsDTO Project);
        Task SaveOrUpdateProjectsAsync(ProjectsDTO ProjectsDTO);

    }
}
