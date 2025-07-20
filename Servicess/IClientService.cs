using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Servicess
{
    public interface IClientService


    {

       
        Task AddClienttAsync(ClientsDTO clientDto);
        //Task AddClienttWithProjectAsync(ClientsDTO clientDto);
        Task<List<ClientsDTO>> GetClientListWithProjectAsync();
        Task<List<ClientsDTO>> GetClientListAsync();





    }
}
