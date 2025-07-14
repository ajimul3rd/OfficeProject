using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class UserTaskService: IUserTaskService
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserTaskService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserTask>> GetTask()
        {
            using var context = dbContextFactory.CreateDbContext();
            return await context.UserTask.ToListAsync();
        }

        public async Task AddOrUpdateTask(UserTask task)
        {
            using var context = dbContextFactory.CreateDbContext();

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