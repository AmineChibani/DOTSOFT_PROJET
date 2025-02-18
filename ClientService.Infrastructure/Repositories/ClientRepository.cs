﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Core.Common;
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
        private readonly AppDbContext _appcontext;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(AppDbContext appcontext, ILogger<ClientRepository> logger)
        {
            _appcontext = appcontext;
            _logger = logger;
        }

        public async Task<DbClient> AddClient(DbClient client)
        {
            await _appcontext.Clients.AddAsync(client);
            return client;
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

        public async Task<List<DbClient>> GetClientsAsync()
        {
            try
            {
                var Allclients = await _appcontext.Clients.AsNoTracking().ToListAsync();
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