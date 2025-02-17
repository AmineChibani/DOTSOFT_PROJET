using ClientService.Core.Entities;

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<DbClient> GetClientById(int IdClient);
        Task<List<DbClient>> GetClients();
    }
}
