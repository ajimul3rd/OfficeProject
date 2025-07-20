using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class OthersServices : IOthersServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataSerializer? DataSerializer;

        public OthersServices(ApplicationDbContext context, IDataSerializer? dataSerializer)
        {
            _context = context;
            DataSerializer = dataSerializer;
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
