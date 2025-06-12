using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace OfficeProject.Servicess
{
    public class WorkingRecordsService : IWorkingRecordsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public WorkingRecordsService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        //(WorkingRecordsDto workingRecordsDto)

        public async Task AddOrUpdateUserWorkingRecordAsync(WorkingRecordsDto workingRecordsDto)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                WorkingRecords? existingRecords = null;
                int workRecordId;

                if (workingRecordsDto.WorkRecordId > 0)
                {
                    existingRecords = await context.WorkRecords.FirstOrDefaultAsync(p => p.WorkRecordId == workingRecordsDto.WorkRecordId);
                }

                if (existingRecords == null)
                {
                    var newRecords = new WorkingRecords
                    {
                        ServiceId = (int)workingRecordsDto.ServiceId,
                        WorkDate = workingRecordsDto.WorkDate,
                        ServiceName = workingRecordsDto.ServiceName,
                        SharedPost = workingRecordsDto.SharedPost,
                        CreatedReels = workingRecordsDto.CreatedReels,
                        UsedAdsBudget = workingRecordsDto.UsedAdsBudget,
                        Task = workingRecordsDto.Task,
                        Status = workingRecordsDto.Status,
                        Remarks = workingRecordsDto.Remarks

                    };

                    context.WorkRecords.Add(newRecords);
                    await context.SaveChangesAsync();
                    workRecordId = newRecords.WorkRecordId; // ✅ get generated ID
                }
                else
                {
                    existingRecords.ServiceId = (int)workingRecordsDto.ServiceId;
                    existingRecords.WorkDate = workingRecordsDto.WorkDate;
                    existingRecords.ServiceName = workingRecordsDto.ServiceName;
                    existingRecords.SharedPost = workingRecordsDto.SharedPost;
                    existingRecords.CreatedReels = workingRecordsDto.CreatedReels;
                    existingRecords.UsedAdsBudget = workingRecordsDto.UsedAdsBudget;
                    existingRecords.Task = workingRecordsDto.Task;
                    existingRecords.Status = workingRecordsDto.Status;
                    existingRecords.Remarks = workingRecordsDto.Remarks;

                    context.WorkRecords.Update(existingRecords);
                    await context.SaveChangesAsync();
                    workRecordId = existingRecords.WorkRecordId;
                }

                // ✅ AssignedUsers Upsert
                foreach (var seoTaskDto in workingRecordsDto.SeoTaskDetailsDto ?? [])
                {
                    try
                    {
                        var existingSeoTask = await context.SeoTaskDetails
                            .FirstOrDefaultAsync(x => x.SeoTaskId == seoTaskDto.SeoTaskId);

                        if (existingSeoTask == null)
                        {
                            context.SeoTaskDetails.Add(new SeoTaskDetails
                            {
                                WorkRecordId = workRecordId,
                                KeyWord = seoTaskDto.KeyWord,
                                CurrentRank = seoTaskDto.CurrentRank,

                            });
                        }
                        else
                        {
                            existingSeoTask.WorkRecordId = workRecordId;
                            existingSeoTask.KeyWord = seoTaskDto.KeyWord;
                            existingSeoTask.CurrentRank = seoTaskDto.CurrentRank;

                            context.SeoTaskDetails.Update(existingSeoTask);
                        }

                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving SeoTaskDetails: {ex.Message}");
                    }
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical failure in AddOrUpdateUserWorkingRecordAsync: {ex.Message}");
                throw;
            }
        }
    }

}
