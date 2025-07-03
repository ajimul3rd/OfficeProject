using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class AssignedUsersService: IAssignedUsersService
    {
        private readonly ApplicationDbContext _context;

        public AssignedUsersService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAssignedUserAsync(int assignedUserId)
        {
            var user = await _context.AssignedUsers.FindAsync(assignedUserId);
            if (user == null)
                return false;

            _context.AssignedUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
