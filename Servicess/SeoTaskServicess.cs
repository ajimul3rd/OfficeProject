using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace OfficeProject.Servicess
{
    public class SeoTaskServicess: ISeoTaskServicess
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public SeoTaskServicess(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        public async Task<bool> SaveOrUpdateSeoTaskAsync(SeoTaskDetailsDto data)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                // Verify WorkRecord exists first
                if (!await context.WorkRecords.AnyAsync(w => w.WorkRecordId == data.WorkRecordId))
                {
                    throw new InvalidOperationException($"WorkRecord with ID {data.WorkRecordId} not found");
                }

                if (data.SeoTaskId > 0) // Update
                {
                    var existing = await context.SeoTaskDetails
                        .FirstOrDefaultAsync(p => p.SeoTaskId == data.SeoTaskId);

                    if (existing != null)
                    {
                        existing.KeyWord = data.KeyWord;
                        existing.CurrentRank = data.CurrentRank;
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
                    var newEntity = new SeoTaskDetails
                    {
                        WorkRecordId = data.WorkRecordId,
                        KeyWord = data.KeyWord,
                        CurrentRank = data.CurrentRank,
                        Note = data.Note,
                        TaskDate = data.TaskDate
                    };
                    context.SeoTaskDetails.Add(newEntity);
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
