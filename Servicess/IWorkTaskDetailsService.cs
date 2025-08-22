using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IWorkTaskDetailsService
    {
        Task AddOrUpdateUserWorkingRecordAsync(WorkTaskDetailsDto workingRecordsDto);
        Task AddOrUpdateBacklinkAsync(WorkTaskDetailsDto workingRecordsDto);
        Task AddOrUpdateClassifiedAsync(WorkTaskDetailsDto workingRecordsDto);
        Task<WorkTaskDetailsDto> GetWorkTaskDetailsById(int workTaskId);
        Task<List<WorkTaskDetailsDto?>> GetWorkingRecordPerUserAsync();
        Task<List<WorkTaskDetailsDto?>> GetWorkingTaskDetailsAsync();
        Task<List<BacklinkDetails>> GetIssuedBacklinkAsync(int serviceId);
        Task<List<ClassifiedDetails>> GetIssuedClassifiedAsync(int serviceId);


    }
}
