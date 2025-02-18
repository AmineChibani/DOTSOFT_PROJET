using ClientService.Core.Common;
using ClientService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClientsAsync();
        Task<DbClient> AddClient(DbClient client);

    }
}
