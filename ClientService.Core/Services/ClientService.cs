using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClientService.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
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

        
        public async Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId)
            {
                if (clientId <= 0)
                {
                    return Result<List<ClientAddressDetailsDto>>.Failure("Invalid client ID provided");
                }

                var result = await _clientRepository.GetAddressesByClientId(clientId);
                if (!result.IsSuccess)
                {
                    return Result<List<ClientAddressDetailsDto>>.Failure(result.Error);
                }

                return Result<List<ClientAddressDetailsDto>>.Success(result.Value);
        }

        public Task<List<VentesNationales>> GetVentesNationales(int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CAResult>> GetCAAsync(CARequest request)
        {
            try
            {
                // Add any business logic/validation here if needed
                if (request.IdClient <= 0)
                {
                    throw new ArgumentException("Invalid Client ID");
                }

                if (request.IdStructure <= 0)
                {
                    throw new ArgumentException("Invalid Structure ID");
                }

                return await _clientRepository.GetCAAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting CA data for client {ClientId}", request.IdClient);
                throw;
            }
        }
    } 
}
