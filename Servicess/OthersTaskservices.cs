using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class OthersTaskservices: IOthersTaskservices
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        private readonly IDataSerializer? DataSerializer;
        public OthersTaskservices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
        }

        public async Task<bool> SaveOrUpdateOthersTaskAsync(OthersTaskDetailsDto data)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                // Verify WorkRecord exists first
                if (!await context.OthersTaskDetails.AnyAsync(w => w.WorkTaskId == data.WorkTaskId))
                {
                    throw new InvalidOperationException($"WorkRecord with ID {data.WorkTaskId} not found");
                }

                if (data.OthersTaskId > 0) // Update
                {
                    var existing = await context.OthersTaskDetails
                        .FirstOrDefaultAsync(p => p.OthersTaskId == data.OthersTaskId);

                    if (existing != null)
                    {
                        existing.LableName = data.LableName;
                        existing.SharedPost = data.SharedPost;
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
                    var newEntity = new OthersTaskDetails
                    {
                        WorkTaskId = data.WorkTaskId,
                        LableName = data.LableName,
                        SharedPost = data.SharedPost,
                        Note = data.Note,
                        TaskDate = data.TaskDate
                    };
                    context.OthersTaskDetails.Add(newEntity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SaveOrUpdateOthersTaskAsync: {ex.Message}");
                throw;
            }
        }


    }
}