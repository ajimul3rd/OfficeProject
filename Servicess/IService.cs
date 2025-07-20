using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IService
    {
        Task<bool> HasNonZeroTotalPost(int serviceId);
        Task<bool> HasNonZeroTotalReels(int serviceId);
        Task<bool> HasNonZeroAdsBudget(int serviceId);
        Task<ServiceFlagsDTO?> GetServiceFlagsAsync(int serviceId);
        Task<bool> UpdateServiceFieldsByProductIdAsync(
        int productsId,
        string serviceName,
        bool isBacklink,
        bool isClasified,
        bool isSocialSharing,
        bool isPost,
        bool isReels,
        bool isAdsBudget);
    }
}
