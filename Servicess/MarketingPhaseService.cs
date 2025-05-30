using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using AutoMapper;
using System.Text.Json;

namespace OfficeProject.Servicess
{
    public class MarketingPhaseService : IMarketingPhaseService
    {


        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public MarketingPhaseService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        public async Task AddMarketingPhaseAsync(MarketingPhaseDTO marketingPhaseDTO)
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


                    if (marketingPhaseDTO.MarketingTaskId != 0)
                    {
                        context.MarketingPhases.Update(Mapper.Map<MarketingPhase>(marketingPhaseDTO));
                    }
                    else
                    {
                        // Create new
                        context.MarketingPhases.Add(Mapper.Map<MarketingPhase>(marketingPhaseDTO));
                    }
                    foreach (var entry in context.ChangeTracker.Entries())
                    {
                        Console.WriteLine($"{entry.Entity.GetType().Name} - {entry.State}");
                    }

                    // Log the DTO as JSON
                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,  // For pretty-printing
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };

                    string json = System.Text.Json.JsonSerializer.Serialize(marketingPhaseDTO, jsonOptions);
                    Console.WriteLine($"marketingPhaseDTO JSON:\n{json}");

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding or updating development task: {ex.Message}");
                throw;
            }
        }
    }
}
