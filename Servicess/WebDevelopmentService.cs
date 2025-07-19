using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class WebDevelopmentService : IWebDevelopmentService
    {
       

    private readonly ApplicationDbContext _context;
        private readonly IDataSerializer? DataSerializer;

        public WebDevelopmentService(ApplicationDbContext context, IDataSerializer? dataSerializer)
        {
            _context = context;
            DataSerializer = dataSerializer;
        }

        public async Task<bool> DeleteWebServiceAsync(int webId)
        {
            var webService = await _context.WebDevelopment.FindAsync(webId);
            if (webService == null)
                return false;

            _context.WebDevelopment.Remove(webService);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
