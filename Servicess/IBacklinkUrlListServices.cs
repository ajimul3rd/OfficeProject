using Microsoft.AspNetCore.Mvc;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Enums;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IBacklinkUrlListServices
    {
        Task AddBacklinkUrlListAsync(List<BacklinkUrlListDTO> dto);
        Task<PagedResult<BacklinkUrlListDTO>> GetBacklinks(int pageNumber = 1, int pageSize = 50, ProjectCategory? category = null);
        Task<bool> SuspendBacklinkAsync(int backlinkUrlId);
        Task SaveOrUpdateBacklinkUrlListAsync(BacklinkUrlListDTO dto);



    }
}
