using ClientService.Core.Entities;

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<DbClient> GetClientById(int IdClient);
        Task<List<DbClient>> GetClientsAsync();
    }
}
