using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using System.Security.Claims;

namespace OfficeProject.Servicess
{
    public class DevelopmentPhaseServices: IDevelopmentPhaseServices
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DevelopmentPhaseServices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task AddDevelopmentPhaseAsync(DevelopmentPhaseDTO developmentPhaseDTO)
        {
            try
            {
                // Get current user ID from claims
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    DevelopmentPhase development;

                    if (developmentPhaseDTO.DevelopmentTaskId != 0)
                    {
                        // Update existing
                        development = await context.DevelopmentPhases
                            .FirstOrDefaultAsync(d => d.DevelopmentTaskId == developmentPhaseDTO.DevelopmentTaskId);

                        if (development == null)
                        {
                            throw new Exception("development task not found.");
                        }
                    }
                    else
                    {
                        // Create new
                        development = new DevelopmentPhase();
                        context.DevelopmentPhases.Add(development);
                    }

                    // Set or update values
                    development.WebDevelopmentId = (int)developmentPhaseDTO.WebDevelopmentId!;
                    development.Title = developmentPhaseDTO.Title;
                    development.Description = developmentPhaseDTO.Description;
                    development.Status = developmentPhaseDTO.Status;
                    development.ProgrammingLanguage= developmentPhaseDTO.ProgrammingLanguage;
                    development.CodeRepoUrl = developmentPhaseDTO.CodeRepoUrl;
                    development.TestStatus = developmentPhaseDTO.TestStatus;                   
                    development.StartTime = developmentPhaseDTO.StartTime;
                    development.EndTime = developmentPhaseDTO.EndTime;
                    development.DeadlineDate = developmentPhaseDTO.DeadlineDate;

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding or updating development task: {ex.Message}");
                throw;
            }
        }

        public async Task<DevelopmentPhaseDTO> GetDevelopmentPhaseAsync(int id)
        {
            try
            {
                // Get current user ID from claims
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    if (id != 0)
                    {
                        var development = await context.DevelopmentPhases
                            .AsNoTracking()
                            .FirstOrDefaultAsync(dp => dp.DevelopmentTaskId == id);

                        if (development == null)
                        {
                            throw new KeyNotFoundException($"DevelopmentPhase with ID {id} not found.");
                        }

                        // Map to DTO
                        var dto = new DevelopmentPhaseDTO
                        {
                            DevelopmentTaskId = development.DevelopmentTaskId,
                            WebDevelopmentId = development.WebDevelopmentId,
                            Title = development.Title,
                            Description = development.Description,
                            ProgrammingLanguage = development.ProgrammingLanguage,
                            CodeRepoUrl = development.CodeRepoUrl,
                            Status = development.Status,
                            TestStatus = development.TestStatus,
                            StartTime = development.StartTime,
                            EndTime = development.EndTime,
                            DeadlineDate = development.DeadlineDate
                        };

                        return dto;
                    }

                    throw new ArgumentException("ID must not be zero.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving development task: {ex.Message}");
                throw;
            }
        }

    }
}
