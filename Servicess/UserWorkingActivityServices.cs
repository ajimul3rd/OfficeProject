using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class UserWorkingActivityServices: IUserWorkingActivityServices
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDataSerializer? DataSerializer;

        public UserWorkingActivityServices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor,
            IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            DataSerializer = dataSerializer;
        }


        public async Task Delete(int Id)
        {
            using var context = dbContextFactory.CreateDbContext();
            var activity = await context.UserWorkingActivity.FindAsync(Id);
            if (activity != null)
            {
                context.UserWorkingActivity.Remove(activity);
                await context.SaveChangesAsync();
            }
        }
    }
}
