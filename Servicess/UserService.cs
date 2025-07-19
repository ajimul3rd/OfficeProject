using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using System.Security.Claims;

namespace OfficeProject.Servicess
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        private readonly IMapper Mapper;

        private readonly IDataSerializer? DataSerializer;

        private readonly IHttpContextAccessor _httpClient;
        public UserService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper, IDataSerializer? dataSerializer, IHttpContextAccessor httpClient)
        {
            this.dbContextFactory = dbContextFactory;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
            _httpClient = httpClient;
        }


        public async Task<List<UserDTO>> GetAllUsersDTOAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var users = await context.Users
                    .Include(u => u.UserDesignation)
                    .ToListAsync();

                var userDtos = users.Select(user => new UserDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserContact = user.UserContact,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    PreeAssignUserRole = user.PreeAssignUserRole,
                    CompanyName = user.CompanyName,
                    Address = user.Address,
                    CreatedAt = (DateTime)user.CreatedAt!,
                    JoiningDate = user.JoiningDate,
                    UserDesignation = user.UserDesignation?.Select(d => new UserDesignation
                    {
                        DesignationId = d.DesignationId,
                        User_Id = d.User_Id,
                        Designation = d.Designation
                    }).ToList() ?? new List<UserDesignation>(),
                    Clients = null // Add mapping for Clients if needed
                }).ToList();

                return userDtos;
            }
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }
        public async Task<UserDTO?> GetUserDTOById(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var user = await context.Users
                    .Include(u => u.UserDesignation)
                    .FirstOrDefaultAsync(u => u.UserId == id);

                if (user == null)
                    return null;

                return new UserDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserContact = user.UserContact,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    PreeAssignUserRole = user.PreeAssignUserRole,
                    CompanyName = user.CompanyName,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt ?? DateTime.UtcNow,
                    JoiningDate = user.JoiningDate,
                    UserDesignation = user.UserDesignation?.Select(d => new UserDesignation
                    {
                        DesignationId = d.DesignationId,
                        User_Id = d.User_Id,
                        Designation = d.Designation
                    }).ToList() ?? new List<UserDesignation>(),
                    Clients = null // Add mapping for Clients if needed
                };
            }
        }
        // ✅ Get User By Id 
        public async Task<Users?> GetUserById(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return await context.Users.FindAsync(id); // Pass the 'id' parameter
            }
        }
        // ✅ Get User and UsersDTO By Name
        public async Task<UserDTO?> GetUserDTOByUsername(string username)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                // Find the user by the provided id (not user.UserId)
                return Mapper.Map<UserDTO>(await context.Users.FirstOrDefaultAsync(u => u.UserName == username));
            }
        }
        public async Task<Users?> GetUserByUsername(string username)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                // Find the user by the provided id (not user.UserId)
                return await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            }
        }
        // ✅ Add a User
        public async Task AddUserAsync(Users user)
        {
            try
            {
                using (var context = dbContextFactory.CreateDbContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateUserAsync(UserDTO user)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                // Load the user along with their existing designations (if needed)
                var existingUser = await context.Users
                    .Include(u => u.UserDesignation) // Include if you want to check existing designations
                    .FirstOrDefaultAsync(u => u.UserId == user.UserId);

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                // Update basic user details
                existingUser.UserName = user.UserName;
                existingUser.UserEmail = user.UserEmail;
                existingUser.UserContact = user.UserContact;
                existingUser.Role = user.Role;
                existingUser.IsActive = user.IsActive;
                existingUser.PreeAssignUserRole = user.PreeAssignUserRole;
                existingUser.CompanyName = user.CompanyName;
                existingUser.Address = user.Address;
                existingUser.JoiningDate = user.JoiningDate;

                await context.SaveChangesAsync(); // Save user changes first

                // Process UserDesignation updates
                if (user.UserDesignation != null)
                {
                    foreach (var activity in user.UserDesignation)
                    {
                        try
                        {
                            // If DesignationId is 0 or null → INSERT new record
                            if (activity.DesignationId == 0)
                            {
                                var newDesignation = new UserDesignation
                                {
                                    User_Id = existingUser.UserId, // Link to the user
                                    Designation = activity.Designation
                                };
                                context.UserDesignation.Add(newDesignation);
                            }
                            else // If DesignationId has a value → UPDATE existing record
                            {
                                var existingDesignation = await context.UserDesignation
                                    .FirstOrDefaultAsync(x => x.DesignationId == activity.DesignationId);

                                if (existingDesignation != null)
                                {
                                    existingDesignation.Designation = activity.Designation;
                                    context.UserDesignation.Update(existingDesignation);
                                }
                                else
                                {
                                    // Handle case where DesignationId was provided but not found
                                    Console.WriteLine($"Designation with ID {activity.DesignationId} not found.");
                                }
                            }

                            await context.SaveChangesAsync(); // Save changes per iteration
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error saving UserDesignation: {ex.Message}");
                        }
                    }
                }
            }
        }
        public async Task UpdateRefreshToken(int userId, string refreshToken)
        {
            using var context = dbContextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays
                    (7);
                await context.SaveChangesAsync();
            }
        }
        // ✅ RevokeRefreshToken
        public async Task RevokeRefreshToken(string username)
        {
            using var context = dbContextFactory.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.RefreshToken = null;
                await context.SaveChangesAsync();
            }
        }
        public async Task<UserDTO?> GetAllUsersDTOClientAsync(UserWithClientsDto userWithClientsDto)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var Designation = new List<UserDesignation>();

                foreach (var data in userWithClientsDto.UserDesignationDto!)
                {
                    Designation.Add(new UserDesignation
                    {
                        DesignationId = (int)data.DesignationId!,
                        Designation = data.Designation
                    });
                }


                // Map DTO to Entity 
                var user = new Users
                {
                    UserName = userWithClientsDto.UserName,
                    UserEmail = userWithClientsDto.UserEmail,
                    UserContact = userWithClientsDto.UserContact,
                    CompanyName = userWithClientsDto.CompanyName,
                    Address = userWithClientsDto.Address,
                    UserDesignation = Designation,
                    JoiningDate = userWithClientsDto.JoiningDate,
                    Role = userWithClientsDto.Role,
                    Clients = userWithClientsDto.Clients.Select(c => new Clients
                    {
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
                        ClientCreatedAt = DateTime.UtcNow
                    }).ToList()
                };

                // Save to DB
                context.Users.Add(user);
                await context.SaveChangesAsync();

                // Return DTO back
                var userDto = new UserDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserContact = user.UserContact,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    Clients = user.Clients.Select(c => new ClientsDTO
                    {
                        ClientId = c.ClientId,
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
                        ClientCreatedAt = (DateTime)c.ClientCreatedAt!
                    }).ToList()
                };

                return userDto;
            }
        }

        public async Task<UserDTO?> GetCurrentLoggedUserAsync()
        {
            try
            {
                var userIdClaim = _httpClient.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    // Get user with designation
                    var user = await context.Users
                        .Include(u => u.UserDesignation)
                        .FirstOrDefaultAsync(u => u.UserId == userId);

                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("User not found.");
                    }


                    return new UserDTO
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Role = user.Role,
                    }
    ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user: {ex.Message}");
                throw;
            }
        }
        public async Task<List<UserDTO>> GetPreeAssignUsersAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var users = await context.Users
                    .Where(u => u.PreeAssignUserRole)
                    .Select(u => new UserDTO
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        Role = u.Role,
                    })
                    .ToListAsync();

                return users;
            }
        }



    }

}
