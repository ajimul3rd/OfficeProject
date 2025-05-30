using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class SeoServicess: ISeoServicess
    {
        private readonly ApplicationDbContext _context;

        public SeoServicess(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSeoDetailsAsync(int seoId)
        {
            var seoService = await _context.SeoServiceDetails.FindAsync(seoId);
            if (seoService == null)
                return false;

            _context.SeoServiceDetails.Remove(seoService);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
