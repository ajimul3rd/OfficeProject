using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class ClientService : IClientService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClientService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task AddClienttAsync(ClientsDTO clientDto)
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    Clients client;

                    if (clientDto.ClientId.HasValue)
                    {
                        // UPDATE existing client
                        client = await context.Client.FindAsync(clientDto.ClientId.Value);

                        if (client == null || client.UserId != userId)
                        {
                            throw new UnauthorizedAccessException("Client not found or access denied");
                        }

                        client.ClientName = clientDto.ClientName;
                        client.CompanyName = clientDto.CompanyName;
                        client.ClientContact1 = clientDto.ClientContact1;
                        client.ClientContact2 = clientDto.ClientContact2;
                        client.ClientEmail1 = clientDto.ClientEmail1;
                        client.ClientEmail2 = clientDto.ClientEmail2;
                        client.ClientAddress = clientDto.ClientAddress;
                        client.ClientCity = clientDto.ClientCity;
                        client.ClientSource = clientDto.ClientSource;
                        client.IssueDate = clientDto.IssueDate;
                        client.IssuedBy = clientDto.IssuedBy;
                        client.IsActive = clientDto.IsActive;
                        // Don't update ClientCreatedAt for updates
                    }
                    else
                    {
                        // ADD new client
                        client = new Clients
                        {
                            UserId = userId,
                            ClientName = clientDto.ClientName,
                            CompanyName = clientDto.CompanyName,
                            ClientContact1 = clientDto.ClientContact1,
                            ClientContact2 = clientDto.ClientContact2,
                            ClientEmail1 = clientDto.ClientEmail1,
                            ClientEmail2 = clientDto.ClientEmail2,
                            ClientAddress = clientDto.ClientAddress,
                            ClientCity = clientDto.ClientCity,
                            ClientSource = clientDto.ClientSource,
                            IssueDate = clientDto.IssueDate,
                            IssuedBy = clientDto.IssuedBy,
                            IsActive = clientDto.IsActive,
                            ClientCreatedAt = clientDto.ClientCreatedAt
                        };

                        await context.Client.AddAsync(client);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving client: {ex.Message}");
                throw;
            }
        }

        //public async Task AddClienttAsync(ClientsDTO clientDto)
        //{
        //    try
        //    {
        //        // Get current user ID from claims
        //        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

        //        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        //        {
        //            throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
        //        }

        //        using (var context = dbContextFactory.CreateDbContext())
        //        {
        //            var client = new Clients
        //            {
        //                UserId = userId,
        //                ClientName = clientDto.ClientName,
        //                CompanyName = clientDto.CompanyName,
        //                ClientContact1 = clientDto.ClientContact1,
        //                ClientContact2 = clientDto.ClientContact2,
        //                ClientEmail1 = clientDto.ClientEmail1,
        //                ClientEmail2 = clientDto.ClientEmail2,
        //                ClientAddress = clientDto.ClientAddress,
        //                ClientCity = clientDto.ClientCity,
        //                ClientSource = clientDto.ClientSource,
        //                IssueDate = clientDto.IssueDate,
        //                IssuedBy = clientDto.IssuedBy,
        //                IsActive = clientDto.IsActive,
        //                ClientCreatedAt = clientDto.ClientCreatedAt
        //            };


        //            context.Client.Add(client);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error adding client: {ex.Message}");
        //        throw;
        //    }
        //}


        public async Task AddClienttWithProjectAsync(ClientsDTO clientDto)
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
                    var client = new Clients
                    {
                        UserId = userId,
                        ClientName = clientDto.ClientName,
                        CompanyName = clientDto.CompanyName,
                        ClientContact1 = clientDto.ClientContact1,
                        ClientContact2 = clientDto.ClientContact2,
                        ClientEmail1 = clientDto.ClientEmail1,
                        ClientEmail2 = clientDto.ClientEmail2,
                        ClientAddress = clientDto.ClientAddress,
                        ClientCity = clientDto.ClientCity,
                        ClientSource = clientDto.ClientSource,
                        IssueDate = clientDto.IssueDate,
                        IssuedBy = clientDto.IssuedBy,
                        IsActive = clientDto.IsActive,
                        ClientCreatedAt = clientDto.ClientCreatedAt,
                        Projects = new List<Projects>()
                    };

                    // Add projects if they exist in the DTO
                    if (clientDto.Projects != null && clientDto.Projects.Any())
                    {
                        foreach (var projectDto in clientDto.Projects)
                        {
                            var project = new Projects
                            {
                                ProjectName = projectDto.ProjectName,
                                ProjectStartDate = projectDto.ProjectStartDate,
                                ProjectType = projectDto.ProjectType,
                                ProjectCost = projectDto.ProjectCost,
                                ProjectCreatedAt = projectDto.ProjectCreatedAt,
                            };

                            // Optionally add WebDevelopment (if provided)
                           

                            // Optionally add MarketingPhases (if provided)


                            client.Projects.Add(project);
                        }
                    }

                    context.Client.Add(client);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding client: {ex.Message}");
                throw;
            }
        }

   
        public async Task<List<ClientsDTO>> GetClientListWithProjectAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    var clients = await context.Client
                        .Include(c => c.Projects)
                        .ToListAsync();

                    var clientDtos = clients.Select(c => new ClientsDTO
                    {
                        ClientId = c.ClientId,
                        UserId = c.UserId,
                        ClientName = c.ClientName,
                        CompanyName = c.CompanyName,
                        ClientContact1 = c.ClientContact1,
                        ClientContact2 = c.ClientContact2,
                        ClientEmail1 = c.ClientEmail1,
                        ClientEmail2 = c.ClientEmail2,
                        ClientAddress = c.ClientAddress,
                        ClientCity = c.ClientCity,
                        ClientSource = c.ClientSource,
                        IsActive = c.IsActive,
                        IssueDate = c.IssueDate,
                        IssuedBy = c.IssuedBy,
                        ClientCreatedAt = (DateTime)c.ClientCreatedAt,
                        Projects = c.Projects.Select(p => new ProjectsDTO
                        {
                            ProjectId = p.ProjectId,
                            ClientId = p.ClientId,
                            ProjectName = p.ProjectName,
                            ProjectStartDate = p.ProjectStartDate,
                            ProjectCost = p.ProjectCost,
                            ProjectType = p.ProjectType,
                            ProjectCreatedAt = p.ProjectCreatedAt
                        }).ToList()
                    }).ToList();

                    return clientDtos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving clients: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ClientsDTO>> GetClientListAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated or invalid user ID");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    var clients = await context.Client
                        .Include(c => c.Projects)
                        .ToListAsync();

                    var clientDtos = clients.Select(c => new ClientsDTO
                    {
                        ClientId = c.ClientId,
                        UserId = c.UserId,
                        ClientName = c.ClientName,
                        CompanyName = c.CompanyName,
                        ClientContact1 = c.ClientContact1,
                        ClientContact2 = c.ClientContact2,
                        ClientEmail1 = c.ClientEmail1,
                        ClientEmail2 = c.ClientEmail2,
                        ClientAddress = c.ClientAddress,
                        ClientCity = c.ClientCity,
                        ClientSource = c.ClientSource,
                        IsActive = c.IsActive,
                        IssueDate = c.IssueDate,
                        IssuedBy = c.IssuedBy,
                        ClientCreatedAt = (DateTime)c.ClientCreatedAt,
                    }).ToList();

                    return clientDtos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving clients: {ex.Message}");
                throw;
            }
        }

    }

}




