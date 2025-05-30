using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public UserService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        // ✅ Get All Users and UsersDTO

        public async Task<List<UserDTO>> GetAllUsersDTOAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return Mapper.Map<List<UserDTO>>(await context.Users.ToListAsync());
            }
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        // ✅ Get UserDTO By Id 
        public async Task<UserDTO?> GetUserDTOById(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var user = await context.Users.FindAsync(id); // Pass the 'id' parameter
                return user == null ? null : Mapper.Map<UserDTO>(user);
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

        // ✅ Update User
        public async Task UpdateUserAsync(UserDTO user)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var existingUser = await context.Users.FindAsync(user.UserId);
                if (existingUser != null)
                {
                    // Update all non-sensitive fields
                    existingUser.UserName = user.UserName;
                    existingUser.UserEmail = user.UserEmail;
                    existingUser.UserContact = user.UserContact;
                    existingUser.Role = user.Role;
                    existingUser.IsActive = user.IsActive;
                    existingUser.CompanyName = user.CompanyName;
                    existingUser.Address = user.Address;
                    existingUser.Designation = user.Designation;
                    existingUser.JoiningDate = user.JoiningDate;

                    // Never update these fields directly from DTO:
                    // - UserPassword (handle separately with hashing)
                    // - RefreshToken (managed by auth system)
                    // - RefreshTokenExpiry (managed by auth system)
                    // - CreatedAt (should never change)
                    // - Clients (navigation property, update separately if needed)

                    await context.SaveChangesAsync();
                }
            }
        }
       
        // ✅ UpdateRefreshToken
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
                // Map DTO to Entity
                var user = new Users
                {
                    UserName = userWithClientsDto.UserName,
                    UserEmail = userWithClientsDto.UserEmail,
                    UserContact = userWithClientsDto.UserContact,
                    CompanyName = userWithClientsDto.CompanyName,
                    Address = userWithClientsDto.Address,
                    Designation = userWithClientsDto.Designation,
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
                        ClientCreatedAt = (DateTime)c.ClientCreatedAt
                    }).ToList()
                };

                return userDto;
            }
        }

    }
}
