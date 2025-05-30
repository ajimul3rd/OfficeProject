namespace OfficeProject.Servicess
{
    public interface IProductVsServices
    {
        Task<bool> DeleteServicesAsync(int ServiceId);
    }
}
