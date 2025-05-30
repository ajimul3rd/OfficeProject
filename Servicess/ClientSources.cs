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

        public async Task Add(ClientSources clientSource)
        {
            using var context = dbContextFactory.CreateDbContext();
            context.clientSources.Add(clientSource);
            await context.SaveChangesAsync();
        }
    }
}
