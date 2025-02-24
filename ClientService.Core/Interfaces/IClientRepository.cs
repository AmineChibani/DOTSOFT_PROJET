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

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<DbClient>> GetClientById(int IdClient);
<<<<<<< HEAD
        Task<Result<List<DbClientAdresse>>> GetAllAdresses();
        Task<Result<List<DbParamPays>>> GetAllPays();
=======
        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
>>>>>>> cefeeb1fc6f3e47c423f66bbbe9bbadd67e7cec3
        Task<List<DbClient>> GetClientsAsync();
        Task<DbClient> AddClient(DbClient client);

        Task<List<VentesNationales>> GetVentesNationales(int clientId);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<IEnumerable<CAResult>> GetCAAsync(CARequest request);
    }
}
