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
<<<<<<< HEAD
using ClientService.Infrastructure.Dtos;
=======
using ClientService.Core.Specifications.Clients;
using ClientService.Core.Common.Pagination;
>>>>>>> 6b67c5c829ce6dc5de4cfbf5a5cd3b7f749299b0

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
<<<<<<< HEAD
        Task<Result<List<DbClient>>> GetClientsAsync();
        Task<int> Create(ClientRequest clientRequest);
=======
        Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter);
        Task<DbClient> AddClient(DbClient client);
>>>>>>> 6b67c5c829ce6dc5de4cfbf5a5cd3b7f749299b0

        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<Result<List<VenteResult>>> GetVentesNationalesAsync(VenteRequest request);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<Result<IEnumerable<CAResult>>> GetCAAsync(CARequest request);
        Task<Result<List<CspDto>>> GetCSPs();
        Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure);
        Task<Result<bool>> DeleteClient(int idClient);
        Task<Result<CommunicationPreferencesBaseDto>> GetClientCommunicationPreferencesAsync(int clientId, int idStructure);
        Task<Result<List<AvoirResult>>> GetAvoirData(int clientId);
    }
}