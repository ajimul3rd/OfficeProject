using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class ProductVsServices: IProductVsServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataSerializer? DataSerializer;

        public ProductVsServices(ApplicationDbContext context, IDataSerializer? dataSerializer)
        {
            _context = context;
            DataSerializer = dataSerializer;
        }
        public async Task<bool> DeleteServicesAsync(int ServiceId)
        {
            var serv = await _context.Services.FindAsync(ServiceId);
            if (serv == null)
                return false;

            _context.Services.Remove(serv);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    
}
