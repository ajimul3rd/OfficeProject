using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IOthersTaskservices
    {
        Task<bool> SaveOrUpdateOthersTaskAsync(OthersTaskDetailsDto data);
    }
}
