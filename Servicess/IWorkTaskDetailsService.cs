using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IWorkTaskDetailsService
    {
        Task AddOrUpdateUserWorkingRecordAsync(WorkTaskDetailsDto workingRecordsDto);
        Task<WorkTaskDetailsDto> GetWorkTaskDetailsById(int workTaskId);

        Task<List<WorkTaskDetailsDto?>> GetWorkingRecordPerUserAsync();
        Task<List<WorkTaskDetailsDto?>> GetWorkingTaskDetailsAsync();
        
    }
}
