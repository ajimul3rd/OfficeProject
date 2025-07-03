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
        private readonly IDataSerializer? DataSerializer;
        public WebDeveTaskService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
        }

        public async Task<bool>  SaveOrUpdateWebTaskAsync(WebDeveTaskDetailsDto data)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                // Verify WorkRecord exists first
                if (!await context.WebDeveTaskDetails.AnyAsync(w => w.WorkTaskId == data.WorkTaskId))
                {
                    throw new InvalidOperationException($"WorkRecord with ID {data.WorkTaskId} not found");
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
                        // Don't update WorkTaskId for existing records
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                else // Create
                {
                    var newEntity = new WebDeveTaskDetails
                    {
                        WorkTaskId = data.WorkTaskId,
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
