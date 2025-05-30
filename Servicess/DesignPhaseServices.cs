using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;

namespace OfficeProject.Servicess
{
    public class DesignPhaseServices : IDesignPhaseServices
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DesignPhaseServices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task AddDesignAsync(DesignPhaseDTO designPhaseDTO)
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
                    DesignPhase design;

                    if (designPhaseDTO.DesignTaskId != 0)
                    {
                        // Update existing
                        design = await context.DesigningPhases
                            .FirstOrDefaultAsync(d => d.DesignTaskId == designPhaseDTO.DesignTaskId);

                        if (design == null)
                        {
                            throw new Exception("Design task not found.");
                        }
                    }
                    else
                    {
                        // Create new
                        design = new DesignPhase();
                        context.DesigningPhases.Add(design);
                    }

                    // Set or update values
                    design.WebDevelopmentId = (int)designPhaseDTO.WebDevelopmentId!;
                    design.Title = designPhaseDTO.Title;
                    design.Description = designPhaseDTO.Description;
                    design.Status = designPhaseDTO.Status;
                    design.DesignTool = designPhaseDTO.DesignTool;
                    design.MockupLink = designPhaseDTO.MockupLink;
                    design.FeedbackStatus = designPhaseDTO.FeedbackStatus;
                    design.StartTime = designPhaseDTO.StartTime;
                    design.EndTime = designPhaseDTO.EndTime;
                    design.DeadlineDate = designPhaseDTO.DeadlineDate;

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding or updating design task: {ex.Message}");
                throw;
            }
        }


        public async Task<DesignPhaseDTO> GetDesignPhaseAsync(int id)
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
                        var design = await context.DesigningPhases
                            .AsNoTracking()
                            .FirstOrDefaultAsync(d => d.DesignTaskId == id);

                        if (design == null)
                        {
                            throw new KeyNotFoundException($"DesignPhase with ID {id} not found.");
                        }

                        // Map to DTO
                        var dto = new DesignPhaseDTO
                        {
                            DesignTaskId = design.DesignTaskId,
                            WebDevelopmentId = design.WebDevelopmentId,
                            Title = design.Title,
                            Description = design.Description,
                            DesignTool = design.DesignTool,
                            MockupLink = design.MockupLink,
                            Status = design.Status,
                            FeedbackStatus = design.FeedbackStatus,
                            StartTime = design.StartTime,
                            EndTime = design.EndTime,
                            DeadlineDate = design.DeadlineDate
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
