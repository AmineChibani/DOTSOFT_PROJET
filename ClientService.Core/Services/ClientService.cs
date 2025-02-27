using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Common.Pagination;
using ClientService.Core.Dtos;
using ClientService.Core.Dtos.ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Core.Mappers;
using ClientService.Core.Specifications.Clients;
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
            if(id <= 0)
            {
              return Result<DbClient>.Failure("Invalid client ID provided");

            }
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

        public async Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter)
        {
            var result = await _clientRepository.GetClientsAsync(filter);
            return result;
            //if (result.Value.Count == 0)
            //{
            //    return Result<List<DbClient>>.Success(result.Value);
            //}
            //return Result<List<DbClient>>.Success(result.Value);
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

        public async Task<Result<List<CspDto>>> GetCSPs()
        {
            var result = await _clientRepository.GetCSPs();
            if (!result.IsSuccess)
            {
                return Result<List<CspDto>>.Failure(result.Error); 
            }
            return Result<List<CspDto>>.Success(result.Value.Select(x => x.toCspDto()).ToList());
        }

        public async Task<Result<IEnumerable<CAResult>>> GetCAAsync(CARequest request)
        {
            if (request == null)
            {
                return Result<IEnumerable<CAResult>>.Failure("Request cannot be null");
            }

            if (request.IdClient <= 0)
            {
                return Result<IEnumerable<CAResult>>.Failure($"Invalid client ID: {request.IdClient}");
            }

            var result = await _clientRepository.GetCAAsync(request);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to retrieve CA data: {Error}", result.Error);
                return Result<IEnumerable<CAResult>>.Failure($"Failed to retrieve CA data: {result.Error}");
            }

            return Result<IEnumerable<CAResult>>.Success(result.Value);
        }

        public async Task<Result<List<VenteResult>>> GetVentesNationalesAsync(VenteRequest request)
        {
            if (request == null)
            {
                return Result<List<VenteResult>>.Failure("Request cannot be null");
            }

            if (request.IdClient <= 0)
            {
                return Result<List<VenteResult>>.Failure($"Invalid client ID: {request.IdClient}");
            }

            var result = await _clientRepository.GetVentesNationalesAsync(request);
            if (result.IsFailure)
            {
                return Result<List<VenteResult>>.Failure($"Failed to retrieve ventes nationales: {result.Error}");
            }

            if (result.Value.Count == 0)
            {
                 return Result<List<VenteResult>>.Failure($"No ventes nationales found for the provided criteria");
            }

            return Result<List<VenteResult>>.Success(result.Value);
        }

        public async Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure)
        {
            var result = await _clientRepository.GetEnCoursAsync(idClient, idStructure);
            if (!result.IsSuccess)
            {
                return Result<List<EnCours>>.Failure("An Error occured while returning Encours" + result.Error);
            }
            return Result<List<EnCours>>.Success(result.Value);
        }

        public async Task<Result<bool>> DeleteClient(int idClient)
        {
            var result = await _clientRepository.DeleteClient(idClient);
            if (!result.IsSuccess)
            {
                return Result<bool>.Failure(result.Error);
            }
            return Result<bool>.Success(result.Value);
        }

        public async Task<Result<GetOptinBaseDto>> GetOptin(int clientId, int idStructure)
        {
            var result = await _clientRepository.GetOptin(clientId,idStructure);
            if (!result.IsSuccess)
            {
                return Result<GetOptinBaseDto>.Failure(result.Error!);
            }
            return Result<GetOptinBaseDto>.Success(result.Value!);
        }

        public async Task<Result<List<AvoirResult>>> GetAvoirData(int clientId)
        {
            if (clientId <= 0)
            {         
                return Result<List<AvoirResult>>.Failure($"Invalid client ID: {clientId}");
            }

            var result = await _clientRepository.GetAvoirData(clientId);
            if (result.IsFailure)
            {
                return Result<List<AvoirResult>>.Failure($"Failed to retrieve avoir data: {result.Error}");
            }


            if (result.Value.Count == 0)
            {
                //return Result<List<AvoirResult>>.Success(result.Value);
                return Result<List<AvoirResult>>.Failure($"No avoir data found for client {clientId}");
            }

            return Result<List<AvoirResult>>.Success(result.Value);
        }
    }
}
