using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class ProductVsServices: IProductVsServices
    {
        private readonly ApplicationDbContext _context;

        public ProductVsServices(ApplicationDbContext context)
        {
            _context = context;
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
