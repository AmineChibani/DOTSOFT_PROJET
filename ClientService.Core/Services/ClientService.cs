using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Interfaces;

namespace ClientService.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<DbClient> GetClientById(int IdClient)
        {
            return await _clientRepository.GetClientById(IdClient);
        }

        public async Task<List<DbClient>> GetClients()
        {
            return await _clientRepository.GetClientsAsync();
        }
    }
}
