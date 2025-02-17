using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace ClientService.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DbClient> GetClientById(int idClient)
        {
            try
            {
                // Using a raw SQL query formatted for Oracle
                var Query = await _context.Clients.FindAsync(idClient);
                return Query;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching client by ID", ex);
            }
        }


        public async Task<List<DbClient>> GetClientsAsync()
        {
            try
            {
                var Allclients = await _context.Clients.AsNoTracking().ToListAsync();
                return Allclients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all clients");
                throw;
            }
        }
    }
}