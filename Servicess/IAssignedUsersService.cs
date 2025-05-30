namespace OfficeProject.Servicess
{
    public interface IAssignedUsersService
    {
        Task<bool> DeleteAssignedUserAsync(int assignedUserId);
    }
}
