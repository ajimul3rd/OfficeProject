using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.Entities; 

namespace OfficeProject.Servicess
{
    public class ClientSourcesService : IClientSources
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClientSourcesService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ClientSources>> GetAll()
        {
            using var context = dbContextFactory.CreateDbContext();
            return await context.clientSources.ToListAsync();  
        }

        public async Task AddOrUpdate(ClientSources clientSource)
        {
            using var context = dbContextFactory.CreateDbContext();

            var existingSource = await context.clientSources
                .FirstOrDefaultAsync(cs => cs.ClientSourcesName == clientSource.ClientSourcesName);

            if (existingSource != null)
            {
                // Update existing
                existingSource.ClientSourcesName = clientSource.ClientSourcesName;
                // update more properties if needed
                context.clientSources.Update(existingSource);
            }
            else
            {
                // Add new
                context.clientSources.Add(clientSource);
            }

            await context.SaveChangesAsync();
        }


        public async Task Delete(int clientSourceId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var source = await context.clientSources.FindAsync(clientSourceId);
            if (source != null)
            {
                context.clientSources.Remove(source);
                await context.SaveChangesAsync();
            }
        }
    }
}
