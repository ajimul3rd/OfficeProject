using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OfficeProject.Servicess
{
    public class UserActivityMasterService : IUserActivityMasterService
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        private readonly IDataSerializer? DataSerializer;
        public UserActivityMasterService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
        }

        public async Task<List<UserActivityMasterDto>> GetAllUserActivityList()
        {
            using var context = dbContextFactory.CreateDbContext();
            var data = await context.UserWorkingActivityList.ToListAsync();
            return   Mapper.Map<List<UserActivityMasterDto>>(data);
        }

        public async Task AddOrUpdateGetUserActivityList(UserActivityMasterDto userWorkingActivity)
        {
            using var context = dbContextFactory.CreateDbContext();

            var existingData = await context.UserWorkingActivityList
                .FirstOrDefaultAsync(d => d.MasterActivityName  == userWorkingActivity.MasterActivityName );

            if (existingData != null)
            {
                // Update existing
                existingData.MasterActivityName  = userWorkingActivity.MasterActivityName ;
                // update more properties if needed
                context.UserWorkingActivityList.Update(existingData);
            }
            else
            {
                // Add new
                context.UserWorkingActivityList.Add(Mapper.Map<UserActivityMaster>(userWorkingActivity));
            }

            await context.SaveChangesAsync();
        }


        public async Task Delete(int Id)
        {
            using var context = dbContextFactory.CreateDbContext();
            var data = await context.UserWorkingActivityList.FindAsync(Id);
            if (data != null)
            {
                context.UserWorkingActivityList.Remove(data);
                await context.SaveChangesAsync();
            }
        }

       
    }
}
