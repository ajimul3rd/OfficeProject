using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IDevelopmentPhaseServices
    {
        Task AddDevelopmentPhaseAsync(DevelopmentPhaseDTO developmentPhaseDTO);

        Task<DevelopmentPhaseDTO> GetDevelopmentPhaseAsync(int id);
    }
}


