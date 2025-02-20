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
            var result = await _clientRepository.GetClientById(id);
            if (!result.IsSuccess)
            {
                return Result<DbClient>.Failure("Error finding the client");
            }
            return Result<DbClient>.Success(result.Value);
        }

        public Task<DbClient> AddClient(DbClient client)
        {
            return _clientRepository.AddClient(client);
        }

        public async Task<List<DbClient>> GetClients()
        {
            return await _clientRepository.GetClientsAsync();
        }

        public async Task<Result<List<DbParamPays>>> GetAllPays()
        {
            var result = await _clientRepository.GetAllPays();
            if (!result.IsSuccess)
            {
                return Result<List<DbParamPays>>.Failure("Error getting countries");
            }
            return Result<List<DbParamPays>>.Success(result.Value);
        }

        public async Task<Result<List<DbClientAdresse>>> GetAddressByClientId(int clientId)
        {
            if (clientId <= 0)
            {
                return Result<List<DbClientAdresse>>.Failure("Invalid client ID provided");
            }

            var result = await _clientRepository.GetAddressByClientId(clientId);
            if (!result.IsSuccess)
            {
                return Result<List<DbClientAdresse>>.Failure($"Error retrieving addresses for client ID {clientId}: {result.Error}");
            }

            if (result.Value.Count == 0)
            {
                return Result<List<DbClientAdresse>>.Success(new List<DbClientAdresse>());
            }

            return Result<List<DbClientAdresse>>.Success(result.Value);
        }
    }
}
