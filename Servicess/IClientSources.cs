using OfficeProject.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeProject.Servicess
{
    public interface IClientSources
    {
        Task<List<ClientSources>> GetAll();
        Task AddOrUpdate(ClientSources clientSource);
        Task Delete(int clientSourceId);
    }
}
