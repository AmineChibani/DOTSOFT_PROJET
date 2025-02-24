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

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClients();
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<DbClient> AddClient(DbClient client);

        Task<Result<List<DbParamPays>>> GetAllPays();

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<Result<List<CspDto>>> GetCSPs();
    }
}
