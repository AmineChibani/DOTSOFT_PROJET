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
        Task<Result<List<DbParamPays>>> GetAllPays();   
        Task<List<DbClient>> GetClientsAsync();
        Task<DbClient> AddClient(DbClient client);
        Task<List<CA>> CA(CaFilter filter);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
    }
}
