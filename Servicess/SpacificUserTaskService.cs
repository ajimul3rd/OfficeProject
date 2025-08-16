
using OfficeProject.Data;

namespace OfficeProject.Servicess
{
    public class SpacificUserTaskService : ISpacificUserTaskService
    {
        private readonly ApplicationDbContext _context;

        public SpacificUserTaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSpacificUserOnProjectAsync(int TaskId)
        {
                       var TasUser= await _context.SpacificUserTask.FindAsync(TaskId);
            if (TasUser == null)
                return false;

            _context.SpacificUserTask.Remove(TasUser);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
