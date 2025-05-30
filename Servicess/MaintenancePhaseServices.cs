using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.Security.Claims;

namespace OfficeProject.Servicess
{
    public class MaintenancePhaseServices: IMaintenancePhaseServices
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MaintenancePhaseServices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task AddMaintenancePhaseAsync(MaintenancePhaseDTO maintenancePhaseDTO)
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
                    MaintenancePhase mainten;

                    if (maintenancePhaseDTO.MaintenanceId != 0)
                    {
                        // Update existing
                        mainten = await context.MaintenancePhase
                            .FirstOrDefaultAsync(m => m.MaintenanceId == maintenancePhaseDTO.MaintenanceId);

                        if (mainten == null)
                        {
                            throw new Exception("development task not found.");
                        }
                    }
                    else
                    {
                        // Create new
                        mainten = new MaintenancePhase();
                        context.MaintenancePhase.Add(mainten);
                    }

                    // Set or update values
                    mainten.WebDevelopmentId = (int)maintenancePhaseDTO.WebDevelopmentId!;
                    mainten.Title = maintenancePhaseDTO.Title;
                    mainten.Description = maintenancePhaseDTO.Description;
                    mainten.Status = maintenancePhaseDTO.Status;
                    mainten.IssueType = maintenancePhaseDTO.IssueType;
                    mainten.Priority = maintenancePhaseDTO.Priority;
                    mainten.SystemAffected = maintenancePhaseDTO.SystemAffected;
                    mainten.StartTime = maintenancePhaseDTO.StartTime;

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding or updating development task: {ex.Message}");
                throw;
            }
        }

      

        public async Task<MaintenancePhaseDTO> GetMaintenancePhaseAsync(int id)
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
                        var maintanance = await context.MaintenancePhase
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.MaintenanceId == id);

                        if (maintanance == null)
                        {
                            throw new KeyNotFoundException($"DesignPhase with ID {id} not found.");
                        }

                        // Map to DTO
                        var dto = new MaintenancePhaseDTO
                        {
                            MaintenanceId = maintanance.MaintenanceId,
                            WebDevelopmentId = maintanance.WebDevelopmentId,
                            Title = maintanance.Title,
                            Description = maintanance.Description,
                            Status = maintanance.Status,
                            IssueType = maintanance.IssueType,
                            Priority = maintanance.Priority,
                            SystemAffected = maintanance.SystemAffected,
                            StartTime = (DateTime)maintanance.StartTime,
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
