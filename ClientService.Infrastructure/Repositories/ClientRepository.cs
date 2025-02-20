using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Core.Specifications.Clients;
using ClientService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            try
            {
                await _appcontext.Clients.AddAsync(client);  
                await _appcontext.SaveChangesAsync();  
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding client to the database");
                throw;  
            }
        }

        public Task<List<CA>> CA(CaFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<DbClientAdresse>>> GetAllAdresses()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<DbParamPays>>> GetAllPays()
        {
            try
            {
                List<DbParamPays> pays = await _appcontext.Pays.ToListAsync();
                return Result<List<DbParamPays>>.Success(pays);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching pays", ex.Message);
                return Result<List<DbParamPays>>.Failure(ex.Message);
            }
        }


        public async Task<Result<DbClient>> GetClientById(int id)
        {
            try
            {
                var client = await _appcontext.Clients.FindAsync(id);
                if (client == null)
                {
                    return Result<DbClient>.Failure("Client not found");
                }
                return Result<DbClient>.Success(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving client with ID {id}");
                return Result<DbClient>.Failure("Error retrieving client");
            }
        }

        public async Task<List<DbClient>> GetClientsAsync()
        {
            try
            {
                var allClients = await _appcontext.Clients.ToListAsync();
                _logger.LogInformation($"Retrieved {allClients.Count} clients from the database");
                return allClients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all clients from the database");
                throw;  
            }
        }

        public async Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId)
        {
            var result = await _appcontext.Set<ClientAddressDetailsDto>()
            .FromSqlRaw("BEGIN GetClientAddresses(:ClientId); END;", new OracleParameter("ClientId", clientId))
            .ToListAsync();

            return Result<List<ClientAddressDetailsDto>>.Success(result);
        }
    }
}