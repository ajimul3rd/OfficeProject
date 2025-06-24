namespace OfficeProject.Servicess
{
    public interface IService
    {
        Task<bool> HasNonZeroTotalPost(int serviceId);
        Task<bool> HasNonZeroTotalReels(int serviceId);
        Task<bool> HasNonZeroAdsBudget(int serviceId);
    }
}
