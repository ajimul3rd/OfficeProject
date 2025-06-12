using OfficeProject.Models.DTO;

namespace OfficeProject.Servicess
{
    public interface IWorkingRecordsService
    {
        Task AddOrUpdateUserWorkingRecordAsync(WorkingRecordsDto workingRecordsDto);
    }
}
