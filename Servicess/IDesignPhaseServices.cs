using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IDesignPhaseServices
    {

        // ✅ Add a design phase for the logged-in user for the logged-in admin
        Task AddDesignAsync(DesignPhaseDTO designPhase);
        Task<DesignPhaseDTO> GetDesignPhaseAsync(int id);

    }
}
