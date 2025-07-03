using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class Service : IService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public Service(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<bool> HasNonZeroTotalPost(int serviceId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var totalPost = await context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => s.TotalPost)
                .FirstOrDefaultAsync();

            return totalPost != 0;
        }

        public async Task<bool> HasNonZeroTotalReels(int serviceId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var totalReels = await context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => s.TotalReels)
                .FirstOrDefaultAsync();

            return totalReels != 0;
        }

        public async Task<bool> HasNonZeroAdsBudget(int serviceId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var adsBudget = await context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => s.AdsBudget)
                .FirstOrDefaultAsync();

            return adsBudget != 0;
        }
    }
}
