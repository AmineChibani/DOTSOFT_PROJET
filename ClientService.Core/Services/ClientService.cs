using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Entities;
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

        public async Task<Result<DbClient>> GetClientById(int id)
        {
            var client = await _clientRepository.GetClientById(id);
            if (!client.IsSuccess)
            {
                return Result<DbClient>.Failure("Client not found");
            }
            return Result<DbClient>.Success(client.Value);
        }
    }
}
