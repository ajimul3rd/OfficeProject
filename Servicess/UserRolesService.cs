using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

        public UserRolesService(
          IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;         

        }
        public async  Task<string> AddOrUpdateUserRoleAsync(UserRoles userRoles)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                if (userRoles == null)
                    return "Invalid input";

                var existingRole = await context.UserRoles
                    .FirstOrDefaultAsync(r => r.RoleId == userRoles.RoleId);

                if (existingRole != null)
                {
                    // Update
                    existingRole.Role = userRoles.Role;
                    context.UserRoles.Update(existingRole);
                }
                else
                {
                    // Add new
                    await context.UserRoles.AddAsync(userRoles);
                }

                await context.SaveChangesAsync();
                return "Saved successfully";
            }



        }
         public async Task<List<UserRoles>> GetAllUserRolesAsync()
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return await context.UserRoles.ToListAsync();
            }
        }
    }
}
