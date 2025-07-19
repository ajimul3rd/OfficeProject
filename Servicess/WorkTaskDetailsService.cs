using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;

namespace OfficeProject.Servicess
{
    public class WorkTaskDetailsService : IWorkTaskDetailsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        private readonly IDataSerializer? DataSerializer;
        public WorkTaskDetailsService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
        }

        //(WorkingRecordsDto workingRecordsDto)

        public async Task AddOrUpdateUserWorkingRecordAsync(WorkTaskDetailsDto workingRecordsDto)
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }


                using var context = dbContextFactory.CreateDbContext();

                WorkTaskDetails? existingRecords = null;
                int WorkTaskId;

                if (workingRecordsDto.WorkTaskId > 0)
                {
                    existingRecords = await context.WorkRecords
                        .FirstOrDefaultAsync(p => 
                        p.WorkTaskId == workingRecordsDto.WorkTaskId &&
                        p.Work_UserId == currentUserId);
                }

                if (existingRecords == null)
                {
                    var newRecords = new WorkTaskDetails
                    {
                        ServiceId = (int)workingRecordsDto.ServiceId,
                        WorkDate = workingRecordsDto.WorkDate,
                        //ServiceName = workingRecordsDto.ServiceName,
                        Work_UserId = currentUserId,
                        SharedPost = workingRecordsDto.SharedPost,
                        CreatedReels = workingRecordsDto.CreatedReels,
                        UsedAdsBudget = workingRecordsDto.UsedAdsBudget,
                        Backlink = workingRecordsDto.Backlink,
                        BacklinkURL = workingRecordsDto.BacklinkURL,
                        Clasified = workingRecordsDto.Clasified,
                        ClasifiedURL = workingRecordsDto.ClasifiedURL,
                        SocialSharing = workingRecordsDto.SocialSharing,
                        SocialSharingURL = workingRecordsDto.SocialSharingURL,
                        Task = workingRecordsDto.Task,
                        Status = workingRecordsDto.Status,
                        Remarks = workingRecordsDto.Remarks

                    };

                    context.WorkRecords.Add(newRecords);
                    await context.SaveChangesAsync();
                    WorkTaskId = newRecords.WorkTaskId; // ✅ get generated ID
                }
                else
                {
                    existingRecords.ServiceId = (int)workingRecordsDto.ServiceId;
                    existingRecords.WorkDate = workingRecordsDto.WorkDate;
                    //existingRecords.ServiceName = workingRecordsDto.ServiceName;
                    existingRecords.SharedPost = workingRecordsDto.SharedPost;
                    existingRecords.CreatedReels = workingRecordsDto.CreatedReels;
                    existingRecords.Backlink = workingRecordsDto.Backlink;
                    existingRecords.BacklinkURL = workingRecordsDto.BacklinkURL;
                    existingRecords.Clasified = workingRecordsDto.Clasified;
                    existingRecords.ClasifiedURL = workingRecordsDto.ClasifiedURL;
                    existingRecords.SocialSharing = workingRecordsDto.SocialSharing;
                    existingRecords.SocialSharingURL = workingRecordsDto.SocialSharingURL;
                    existingRecords.UsedAdsBudget = workingRecordsDto.UsedAdsBudget;
                    existingRecords.Task = workingRecordsDto.Task;
                    existingRecords.Status = workingRecordsDto.Status;
                    existingRecords.Remarks = workingRecordsDto.Remarks;

                    context.WorkRecords.Update(existingRecords);
                    await context.SaveChangesAsync();
                    WorkTaskId = existingRecords.WorkTaskId;
                }


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
                                WorkTaskId = WorkTaskId,
                                KeyWord = seoTaskDto.KeyWord,
                                CurrentRank = seoTaskDto.CurrentRank,
                                Note = seoTaskDto.Note,

                            });
                        }
                        else
                        {
                            existingSeoTask.WorkTaskId = WorkTaskId;
                            existingSeoTask.KeyWord = seoTaskDto.KeyWord;
                            existingSeoTask.CurrentRank = seoTaskDto.CurrentRank;
                            existingSeoTask.Note = existingSeoTask.Note;

                            context.SeoTaskDetails.Update(existingSeoTask);
                        }

                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving SeoTaskDetails: {ex.Message}");
                    }
                }
                foreach (var othersTaskDto in workingRecordsDto.OthersTaskDetailsDto ?? [])
                {
                    try
                    {
                        var existingOthersTask = await context.OthersTaskDetails
                            .FirstOrDefaultAsync(x => x.OthersTaskId == othersTaskDto.OthersTaskId);

                        if (existingOthersTask == null)
                        {
                            context.OthersTaskDetails.Add(new OthersTaskDetails
                            {
                                WorkTaskId = WorkTaskId,
                                LableName = othersTaskDto.LableName,
                                SharedPost = othersTaskDto.SharedPost,
                                Note = othersTaskDto.Note

                            });
                        }
                        else
                        {
                            existingOthersTask.WorkTaskId = WorkTaskId;
                            existingOthersTask.LableName = othersTaskDto.LableName;
                            existingOthersTask.SharedPost = othersTaskDto.SharedPost;
                            existingOthersTask.Note = existingOthersTask.Note;
                            context.OthersTaskDetails.Update(existingOthersTask);
                        }

                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving SeoTaskDetails: {ex.Message}");
                    }
                }

                foreach (var webTaskDto in workingRecordsDto.WebDeveTaskDetailsDto ?? [])
                {
                    try
                    {
                        var existingWebTask = await context.WebDeveTaskDetails
                            .FirstOrDefaultAsync(x => x.webDevTaskId == webTaskDto.webDevTaskId);

                        if (existingWebTask == null)
                        {
                            context.WebDeveTaskDetails.Add(new WebDeveTaskDetails
                            {
                                WorkTaskId = WorkTaskId,
                                Task = webTaskDto.Task,
                                Remarks = webTaskDto.Remarks,
                                Note = webTaskDto.Note


                            });
                        }
                        else
                        {
                            existingWebTask.WorkTaskId = WorkTaskId;
                            existingWebTask.Task = webTaskDto.Task;
                            existingWebTask.Remarks = webTaskDto.Remarks;
                            existingWebTask.Note = webTaskDto.Note;
                            context.WebDeveTaskDetails.Update(existingWebTask);
                        }

                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving SeoTaskDetails: {ex.Message}");
                    }
                }
                //Console.WriteLine("workingRecordsDto.ProjectId:"+ workingRecordsDto.ProjectId);
                //Console.WriteLine("workingRecordsDto.Task:" + workingRecordsDto.Status);
                if (workingRecordsDto.ProjectId > 0&& workingRecordsDto.Status == "Completed")
                {
                    await UpdateProjectsUserWorkDoneFlagAsync((int)workingRecordsDto.ProjectId);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical failure in AddOrUpdateUserWorkingRecordAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProjectsUserWorkDoneFlagAsync(int ProjectId)
        {
            try
            {
                Console.WriteLine("UpdateProjectsUserWorkDoneFlagAsync:");
                using var context = dbContextFactory.CreateDbContext();

                Projects? existingProject = null;

                if (ProjectId > 0)
                {
                    existingProject = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == ProjectId);
                }

                if (existingProject != null)
                {
                    if (existingProject.ProjectType == ProjectType.EXTRA_SERVICE)
                    {
                        existingProject.IsUserWorkDone = true;
                        context.Projects.Update(existingProject);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        // Optionally handle updates for non-EXTRA_SERVICE types if needed
                        Console.WriteLine($"Project {existingProject.ProjectId} is not of type EXTRA_SERVICE, no update performed.");
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical failure in UpdateProjectsAsync: {ex.Message}");
                throw;
            }
        }






        //public Task<List<WorkTaskDetailsDto?>> GetWorkingRecordPerUserAsync()
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<List<WorkTaskDetailsDto?>> GetWorkingRecordPerUserAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                using var context = dbContextFactory.CreateDbContext();

                var workTaskDtos = await context.WorkRecords
                    .Where(w => w.Work_UserId == currentUserId)
                    .Select(w => new WorkTaskDetailsDto
                    {
                        WorkTaskId = w.WorkTaskId,
                        ServiceId = w.ServiceId,
                        ServiceName = w.Services != null ? w.Services.ServiceName : null, // requires navigation property
                        WorkDate = w.WorkDate,
                        SharedPost = w.SharedPost,
                        CreatedReels = w.CreatedReels,
                        UsedAdsBudget = w.UsedAdsBudget,
                        Task = w.Task,
                        Status = w.Status,
                        Remarks = w.Remarks,

                        SeoTaskDetailsDto = w.SeoTaskDetails.Select(s => new SeoTaskDetailsDto
                        {
                            SeoTaskId = s.SeoTaskId,
                            KeyWord = s.KeyWord,
                            CurrentRank = s.CurrentRank,
                            Note = s.Note
                        }).ToList(),

                        OthersTaskDetailsDto = w.OthersTaskDetails.Select(o => new OthersTaskDetailsDto
                        {
                            OthersTaskId = o.OthersTaskId,
                            LableName = o.LableName,
                            SharedPost = o.SharedPost,
                            Note = o.Note
                        }).ToList(),

                        WebDeveTaskDetailsDto = w.WebDeveTaskDetails.Select(wd => new WebDeveTaskDetailsDto
                        {
                            webDevTaskId = wd.webDevTaskId,
                            Task = wd.Task,
                            Remarks = wd.Remarks,
                            Note = wd.Note
                        }).ToList()
                    }).ToListAsync();

                //DataSerializer.Serializer(workTaskDtos, "WorkTaskDetailsService:GetWorkingRecordPerUserAsync:");

                return workTaskDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetWorkingRecordPerUserAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<WorkTaskDetailsDto?>> GetWorkingTaskDetailsAsync()
        {
            using var context = dbContextFactory.CreateDbContext();

            try
            {
                var workTaskDtos = await context.WorkRecords
                    .AsNoTracking()
                    .Select(wr => new WorkTaskDetailsDto
                    {
                        WorkTaskId = wr.WorkTaskId,
                        ServiceId = wr.ServiceId,
                        WorkDate = wr.WorkDate,
                        Task = wr.Task,
                        Remarks = wr.Remarks,
                        Work_UserId=wr.Work_UserId
                    })
                    .ToListAsync();

                DataSerializer.Serializer(workTaskDtos, "WorkTaskDetailsService:GetWorkingTaskDetailsAsync:");

                return workTaskDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetWorkingTaskDetailsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<WorkTaskDetailsDto> GetWorkTaskDetailsById(int workTaskId)
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }


                using var context = dbContextFactory.CreateDbContext();

                // Get the main work task record
                var workTask = await context.WorkRecords
                    .FirstOrDefaultAsync(w => w.WorkTaskId == workTaskId &&
                    w.Work_UserId == currentUserId);

                if (workTask == null)
                {
                    return null;
                }

                // Map to DTO
                var workTaskDto = Mapper.Map<WorkTaskDetailsDto>(workTask);

                // Get related SEO tasks
                workTaskDto.SeoTaskDetailsDto = await context.SeoTaskDetails
                    .Where(s => s.WorkTaskId == workTaskId)
                    .Select(s => new SeoTaskDetailsDto
                    {
                        SeoTaskId = s.SeoTaskId,
                        KeyWord = s.KeyWord,
                        CurrentRank = s.CurrentRank,
                        Note = s.Note
                    })
                    .ToListAsync();

                // Get related Others tasks
                workTaskDto.OthersTaskDetailsDto = await context.OthersTaskDetails
                    .Where(o => o.WorkTaskId == workTaskId)
                    .Select(o => new OthersTaskDetailsDto
                    {
                        OthersTaskId = o.OthersTaskId,
                        LableName = o.LableName,
                        SharedPost = o.SharedPost,
                        Note = o.Note
                    })
                    .ToListAsync();

                // Get related Web Development tasks
                workTaskDto.WebDeveTaskDetailsDto = await context.WebDeveTaskDetails
                    .Where(w => w.WorkTaskId == workTaskId)
                    .Select(w => new WebDeveTaskDetailsDto
                    {
                        webDevTaskId = w.webDevTaskId,
                        Task = w.Task,
                        Remarks = w.Remarks,
                        Note = w.Note
                    })
                    .ToListAsync();

                return workTaskDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetWorkTaskDetailsById: {ex.Message}");
                throw;
            }
        }       
    }
}
