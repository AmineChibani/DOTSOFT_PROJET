using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<DbClient> GetClientById(int IdClient);
        Task<List<DbClient>> GetClients();
    }
}
