using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Infrastructure.Data;

namespace ClientService.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _appcontext;

        public ClientRepository(AppDbContext appcontext)
        {
            _appcontext = appcontext;
        }

        public async Task<Result<DbClient>> GetClientById(int id)
        {
            var client = await _appcontext.Clients.FindAsync(id);
            if (client == null)
            {
                return Result<DbClient>.Failure("Client not found");
            }
            return Result<DbClient>.Success(client);
        }
    }
}
