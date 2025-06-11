using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class UserDesignationService: IUserDesignationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserDesignationService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task Delete(int Id)
        {
            using var context = dbContextFactory.CreateDbContext();
            var activity = await context.UserDesignation.FindAsync(Id);
            if (activity != null)
            {
                context.UserDesignation.Remove(activity);
                await context.SaveChangesAsync();
            }
        }
    }
}

