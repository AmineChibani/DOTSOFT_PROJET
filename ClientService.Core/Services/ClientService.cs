﻿using System;
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
using ClientService.Infrastructure.Dtos;
using ClientService.Core.Specifications.Clients;
using Microsoft.Extensions.Logging;
using Azure.Core;

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

        public async Task<Result<ClientRequest>> GetClientById(int id)
        {
            if (id <= 0)
            {
                return Result<ClientRequest>.Failure("Invalid client ID provided");

            }
            var result = await _clientRepository.GetClientByIdAsync(id);
            if (!result.IsSuccess || result.Value == null)
            {
                return Result<ClientRequest>.Failure("Error finding the client");
            }
            return Result<ClientRequest>.Success(result.Value.ToClientRequest());
        }



        public async Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter)
        {
            var result = await _clientRepository.GetClientsAsync(filter);
            if (!result.IsSuccess)
            {
                return Result<PagedResult<ClientDto>>.Failure("No clients found.");
            }

            return Result<PagedResult<ClientDto>>.Success(result.Value);
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

        public async Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId, int libelleCode)
        {
            if (clientId <= 0)
            {
                return Result<List<ClientAddressDetailsDto>>.Failure("Invalid client ID provided");
            }

            var result = await _clientRepository.GetAddressesByClientId(clientId, libelleCode);
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
            var result = await _clientRepository.GetOptin(clientId, idStructure);
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

        public async Task<int> Create(ClientRequest clientRequest)
        {
            ValidateClientRequest(clientRequest);

            return await _clientRepository.Create(clientRequest);
        }
        public void ValidateClientRequest(ClientRequest clientRequest)
        {
            if (clientRequest == null)
                throw new ArgumentNullException(nameof(clientRequest), "Client request cannot be null.");

            if (string.IsNullOrWhiteSpace(clientRequest.FirstName))
                throw new ArgumentException("First name is required.", nameof(clientRequest.FirstName));

            if (string.IsNullOrWhiteSpace(clientRequest.LastName))
                throw new ArgumentException("Last name is required.", nameof(clientRequest.LastName));

        }

        public async Task<List<HistoVentesResult>> GetHistoVentes(int clientId)
        {
            try
            {
                var result = await _clientRepository.GetHistoVentes(clientId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales history for client {ClientId}", clientId);
                throw new Exception("An error occurred while fetching the sales history.", ex);
            }
        }

        public async Task<List<DbParamRegion>> GetRegions(int? paysId)
        {
            if (paysId == null)
            {
                _logger.LogWarning("GetRegions called with null paysId.");
                return new List<DbParamRegion>();
            }

            var regions = await _clientRepository.GetRegions(paysId);

            if (!regions.Any())
            {
                _logger.LogInformation($"No regions found for paysId: {paysId}");
            }

            return regions;
        }
        public async Task<Result<bool>> UpdateClientAsync(int clientId, ClientRequest request)
        {
            var result = await _clientRepository.GetClientByIdAsync(clientId);
            if (!result.IsSuccess)
            {
                return Result<bool>.Failure(result.Error!);
            }

            var client = result.Value;

            // Update basic client fields
            client.Prenom = request.FirstName ?? client.Prenom;
            client.Nom = request.LastName ?? client.Nom;
            client.Nom2 = request.LastName2 ?? client.Nom2;
            client.EmployeId = request.EmployeId ?? client.EmployeId;
            client.RaisonSociale = request.RaisonSociale ?? client.RaisonSociale;
            client.StructureId = request.StructureId;
            client.OkPourSms = request.OkPourSms == true ? 1 : 0;
            client.OkPourMailing = request.OkPourMailing == true ? 1 : 0;
            client.OkPourMailingAff = request.OkPourMailingAff == true ? 1 : 0;
            client.OkPourSmsPartner = request.OkPourSmsPartner == true ? 1 : 0;
            client.OkPourSmsAff = request.OkPourSmsAff == true ? 1 : 0;
            client.Eticket = request.Eticket == true ? 1 : 0;
            client.Particulier = request.Particulier == true ? 1 : 0;

            // Update addresses
            if (request.ClientAdressesRequest != null && request.ClientAdressesRequest.Any())
            {
                client.ClientAdresses = request.ClientAdressesRequest.Select(a => new DbClientAdresse
                {
                    AdresseTypeId = a.AdresseTypeId,
                    Adresse1 = a.Adresse1,
                    CpId = a.CpId,
                    PaysId = a.PaysId,
                    CpEtranger = a.CpEtranger,
                    VilleEtranger = a.VilleEtranger,
                    Btqc = a.Btqc,
                    Batesc = a.Batesc,
                    PhoneNumber = a.PhoneNumber,
                    CellPhone = a.CellPhone,
                    Fax = a.Fax
                }).ToList();
            }

            // Update address complements
            if (request.ClientAdresseComplementRequest != null && request.ClientAdresseComplementRequest.Any())
            {
                client.ClientAdresseComplement = request.ClientAdresseComplementRequest.Select(c => new DbClientAdresseComplement
                {
                    AdresseTypeId = c.AdresseTypeId,
                    OkPourEnvoiPostal = c.OkPourEnvoiPostal == true ? 1 : 0,
                    OkPourEnvoiPostalAff = c.OkPourEnvoiPostalAff == true ? 1 : 0,
                    OkPourEnvoiPostalPartner = c.OkPourEnvoiPostalPartner == true ? 1 : 0
                }).ToList();
            }

            // Update client opt-in data
            if (request.ClientOptinRequest != null)
            {
                client.ClientOptin = new DbClientOptin
                {
                    DateOptinEmail = request.ClientOptinRequest.DateOptinEmail,
                    DateOptinPostal = request.ClientOptinRequest.DateOptinPostal,
                    DateOptinSms = request.ClientOptinRequest.DateOptinSms,
                    DateAffOptinEmail = request.ClientOptinRequest.DateAffOptinEmail,
                    DateAffOptinPostal = request.ClientOptinRequest.DateAffOptinPostal,
                    DateAffOptinSms = request.ClientOptinRequest.DateAffOptinSms,
                    DatePartnerOptinEmail = request.ClientOptinRequest.DatePartnerOptinEmail,
                    DatePartnerOptinPostal = request.ClientOptinRequest.DatePartnerOptinPostal,
                    DatePartnerOptinSms = request.ClientOptinRequest.DatePartnerOptinSms
                };
            }

            await _clientRepository.UpdateAsync(client);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<LoyaltyCardDto>>> GetLoyaltyCardInfoAsync(long idCarte)
        {
            try
            {
                var result = await _clientRepository.GetLoyaltyCardInfoAsync(idCarte);

                if (!result.IsSuccess || result.Value == null || result.Value.Count == 0)
                    return Result<List<LoyaltyCardDto>>.Failure($"No loyalty card found with ID: {idCarte}");

                return Result<List<LoyaltyCardDto>>.Success(result.Value);
            }
            catch (Exception ex)
            {
                return Result<List<LoyaltyCardDto>>.Failure($"Failed to retrieve loyalty card: {ex.Message}");
            }
        }

    }

}
