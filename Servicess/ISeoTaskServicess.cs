using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface ISeoTaskServicess
    {
        Task<bool> SaveOrUpdateSeoTaskAsync(SeoTaskDetailsDto data);
    }
}
