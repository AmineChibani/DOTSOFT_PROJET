using ClientService.Core.Common;
using ClientService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Entities;
using ClientService.Core.Dtos;
using ClientService.Core.Specifications.Clients;
using ClientService.Core.Dtos;
using ClientService.Core.Dtos.ClientService.Core.Dtos;
using ClientService.Infrastructure.Dtos;
using ClientService.Core.Common.Pagination;

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<DbClient>> GetClientById(int IdClient);
        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter);
        Task<int> Create(ClientRequest clientRequest);
        Task<Result<List<VenteResult>>> GetVentesNationalesAsync(VenteRequest request);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<Result<IEnumerable<CAResult>>> GetCAAsync(CARequest request);
        Task<Result<List<DbParamCategSocioProf>>> GetCSPs();

        Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure);

        Task<Result<bool>> DeleteClient(int idClient);

        Task<Result<CommunicationPreferencesBaseDto>> GetClientCommunicationPreferencesAsync(int clientId, int idStructure);
        Task<Result<List<AvoirResult>>> GetAvoirData(int clientId);
        Task<List<HistoVentesResult>> GetHistoVentes(int clientId);
    }
}