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

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClients();
        Task<DbClient> AddClient(DbClient client);

        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<IEnumerable<CAResult>> GetCAAsync(CARequest request);

        Task<Result<List<CspDto>>> GetCSPs();
        Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure);
        Task<Result<bool>> DeleteClient(int idClient);
        Task<Result<CommunicationPreferencesDto>> GetClientCommunicationPreferencesAsync(int clientId);
        Task<Result<List<AvoirResult>>> GetAvoirData(int clientId);
    }
}