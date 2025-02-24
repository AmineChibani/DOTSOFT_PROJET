using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Dtos;
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

        public async Task<Result<int>> Duplicate(int clientId, int adressTypeId)
        {
            if (clientId <= 0)
            {
                return Result<int>.Failure("Invalid client ID provided");
            }

            var result = await _clientRepository.Duplicate(clientId, adressTypeId);
            if (!result.IsSuccess)
            {
                return Result<int>.Failure(result.Error);
            }

            return Result<int>.Success(result.Value);

        }


        //Get chiffre d'affaire par client (ou bien tous les factures de Client )
        public async Task<IEnumerable<CAResult>> GetCAAsync(CARequest request)
        {
            try
            {
                if (request.IdClient <= 0)
                {
                    throw new ArgumentException("Invalid Client ID");
                }

                return await _clientRepository.GetCAAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting CA data for client {ClientId}", request.IdClient);
                throw;
            }
        }

        public async Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request)
        {
            try
            {
                // Input validation
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.IdClient <= 0)
                {
                    _logger.LogWarning("Invalid client ID provided: {ClientId}", request.IdClient);
                    throw new ArgumentException("Invalid client ID provided", nameof(request.IdClient));
                }

                // Additional validation if needed
                if (request.Abandonnee != 0 && request.Abandonnee != 1)
                {
                    _logger.LogWarning("Invalid Abandonnee value provided: {Abandonnee}", request.Abandonnee);
                    throw new ArgumentException("Abandonnee value must be 0 or 1", nameof(request.Abandonnee));
                }

                // Call repository method
                var results = await _clientRepository.GetVentesNationalesAsync(request);

                // Log success
                _logger.LogInformation("Successfully retrieved national sales data for client {ClientId}", request.IdClient);

                return results;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error in GetVentesNationalesAsync for client {ClientId}", request.IdClient);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing national sales data for client {ClientId}", request.IdClient);
                throw;
            }
        }
    }
}
