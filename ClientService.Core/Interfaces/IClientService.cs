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
using ClientService.Core.Dtos;
using ClientService.Core.Dtos.ClientService.Core.Dtos;
using ClientService.Infrastructure.Dtos;
using ClientService.Core.Common.Pagination;
using ClientService.Core.Specifications.Clients;
using ClientService.Core.Specifications.Clients;
using ClientService.Core.Common.Pagination;

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<int> Create(ClientRequest clientRequest);
        Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter);
        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<Result<List<VenteResult>>> GetVentesNationalesAsync(VenteRequest request);
        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId, int libelleCode);
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<Result<IEnumerable<CAResult>>> GetCAAsync(CARequest request);
        Task<Result<List<CspDto>>> GetCSPs();
        Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure);
        Task<Result<bool>> DeleteClient(int idClient);
        Task<Result<List<AvoirResult>>> GetAvoirData(int clientId);
        Task<List<HistoVentesResult>> GetHistoVentes(int clientId);
        Task<Result<GetOptinBaseDto>> GetOptin(int clientId, int idStructure);
        Task<List<DbParamRegion>> GetRegions(int? paysId);
        Task<Decimal?> GetMontantCredit(int clientId, int structureId);
        Task<bool> UpdateClientAsync(int clientId, ClientRequest request);

    }
}