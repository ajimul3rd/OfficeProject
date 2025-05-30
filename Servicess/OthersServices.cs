using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class OthersServices : IOthersServices
    {
        private readonly ApplicationDbContext _context;

        public OthersServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOtherServiceAsync(int otherServiceId)
        {
            var service = await _context.OthersService.FindAsync(otherServiceId);
            if (service == null)
                return false;

            _context.OthersService.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
