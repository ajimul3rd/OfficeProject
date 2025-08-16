namespace OfficeProject.Servicess
{
    public interface ISpacificUserTaskService
    {
        Task<bool> DeleteSpacificUserOnProjectAsync(int TaskId);
    }
}
