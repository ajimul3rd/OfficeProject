using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.Entities;
using System.Net.Http;
using System.Security.Claims;

namespace OfficeProject.Servicess
{
    public class UserTaskMasterService : IUserTaskMasterService
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;


        private readonly IDataSerializer? DataSerializer;

        public UserTaskMasterService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IDataSerializer? DataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.DataSerializer = DataSerializer;
        }

        public async Task<List<UserTaskMaster>> GetUserTasksMasterAsync()
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            using var context = dbContextFactory.CreateDbContext();

            return await context.UserTask.Where(task => task.UserTask_UserId == userId).ToListAsync();
        }

        public async Task AddOrUpdateUserTasksMasterAsync(UserTaskMaster task)
        {

          
            using var context = dbContextFactory.CreateDbContext();
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var existingTask = await context.UserTask
                .FirstOrDefaultAsync(cs => cs.UserTaskName == task.UserTaskName);

            if (existingTask != null)
            {
                // Update existing
                existingTask.UserTaskName = task.UserTaskName;
                // update more properties if needed
                context.UserTask.Update(existingTask);
            }
            else
            {
                // Add new
                task.UserTask_UserId = userId; // ✅ assign userId here
                context.UserTask.Add(task);
            }

            await context.SaveChangesAsync();
        }


        public async Task DeleteTask(int taskId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var source = await context.UserTask.FindAsync(taskId);
            if (source != null)
            {
                context.UserTask.Remove(source);
                await context.SaveChangesAsync();
            }
        }


    }
}