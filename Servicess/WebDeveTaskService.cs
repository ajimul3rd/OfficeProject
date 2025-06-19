using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class WebDeveTaskService : IWebDeveTaskService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public WebDeveTaskService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        public async Task<bool>  SaveOrUpdateWebTaskAsync(WebDeveTaskDetailsDto data)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                // Verify WorkRecord exists first
                if (!await context.WebDeveTaskDetails.AnyAsync(w => w.WorkRecordId == data.WorkRecordId))
                {
                    throw new InvalidOperationException($"WorkRecord with ID {data.WorkRecordId} not found");
                }

                if (data.webDevTaskId> 0) // Update
                {
                    var existing = await context.WebDeveTaskDetails
                        .FirstOrDefaultAsync(p => p.webDevTaskId == data.webDevTaskId);

                    if (existing != null)
                    {
                        existing.Task = data.Task;
                        existing.Remarks = data.Remarks;
                        existing.Note = data.Note;
                        existing.TaskDate = data.TaskDate;
                        // Don't update WorkRecordId for existing records
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                else // Create
                {
                    var newEntity = new WebDeveTaskDetails
                    {
                        WorkRecordId = data.WorkRecordId,
                        Task = data.Task,
                        Remarks = data.Remarks,
                        Note = data.Note,
                        TaskDate = data.TaskDate,
                    };
                    context.WebDeveTaskDetails.Add(newEntity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveOrUpdateSeoTaskAsync: {ex.Message}");
                throw;
            }
        }

        
    }
}
