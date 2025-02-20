using System;
using System.Collections.Generic;
using System.Data;
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

        public Task<List<CA>> GetClientCA(int clientId, int structureId)
        {
            try
            {
                var results = await _context.GetCAAsync(
                    clientId: clientId,
                    structureId: structureId,
                    startDate: DateTime.Now.AddMonths(-1),
                    endDate: DateTime.Now,
                    all: 0);

                return results;
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine($"Error fetching CA: {ex.Message}");
                throw;
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
            var resultList = new List<ClientAddressDetailsDto>();

            using (var connection = _appcontext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "BEGIN DOTSOFT.GetClientAddresses(:ClientId, :ResultCursor); END;";
                    command.CommandType = CommandType.Text;

                    // Input parameter
                    var clientIdParam = new OracleParameter("ClientId", OracleDbType.Int32)
                    {
                        Value = clientId,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(clientIdParam);

                    // Output parameter (REF CURSOR)
                    var cursorParam = new OracleParameter("ResultCursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                return Result<List<DbClientAdresse>>.Success(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching addresses for client ID {clientId}", ex.Message);
                return Result<List<DbClientAdresse>>.Failure(ex.Message);
            }

            return Result<List<ClientAddressDetailsDto>>.Success(resultList);
        }

    }
}