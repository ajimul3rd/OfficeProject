using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;

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

        public async Task<bool> UpdateServiceFieldsByProductIdAsync(
            int productsId,
            string serviceName,
            bool isBacklink,
            bool isClasified,
            bool isSocialSharing,
            bool isPost,
            bool isReels,
            bool isAdsBudget)
        {
            using var context = dbContextFactory.CreateDbContext();

            var services = await context.Services
                .Where(s => s.ProductsId == productsId)
                .ToListAsync();

            if (!services.Any())
            {
                return false; // Or throw NotFoundException if you prefer
            }

            foreach (var service in services)
            {
                service.ServiceName = serviceName;
                service.IsBacklink = isBacklink;
                service.IsClasified = isClasified;
                service.IsSocialSharing = isSocialSharing;
                service.IsPost = isPost;
                service.IsReels = isReels;
                service.IsAdsBudget = isAdsBudget;
            }

            await context.SaveChangesAsync();
            return true;
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
        public async Task<ServiceFlagsDTO?> GetServiceFlagsAsync(int serviceId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var flags = await context.Services
                .Where(s => s.ServiceId == serviceId)
                .Select(s => new ServiceFlagsDTO
                {
                    IsBacklink = s.IsBacklink,
                    IsClasified = s.IsClasified,
                    IsSocialSharing = s.IsSocialSharing,
                    IsPost = s.IsPost,
                    IsReels = s.IsReels,
                    IsAdsBudget = s.IsAdsBudget
                })
                .FirstOrDefaultAsync();

            return flags; // returns null if not found
        }

    }
}
