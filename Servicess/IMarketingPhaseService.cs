using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IMarketingPhaseService
    {
        Task AddMarketingPhaseAsync(MarketingPhaseDTO marketingPhaseDTO);
    }
}
