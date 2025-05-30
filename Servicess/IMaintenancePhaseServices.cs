using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IMaintenancePhaseServices
    {
        Task AddMaintenancePhaseAsync(MaintenancePhaseDTO maintenancePhaseDTO);

        Task<MaintenancePhaseDTO> GetMaintenancePhaseAsync(int id);
    }
}
