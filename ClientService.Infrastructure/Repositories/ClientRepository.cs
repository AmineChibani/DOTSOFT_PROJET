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

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultList.Add(new ClientAddressDetailsDto
                            {
                                IdClient = reader.GetInt32(0),
                                IdTypeAdresse = reader.GetInt32(1),
                                Adresse1 = reader.GetString(2),
                                Adresse2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                                IdCp = reader.GetInt32(4),
                                IdPays = reader.GetInt32(5),
                                Telephone = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Portable = reader.IsDBNull(7) ? null : reader.GetString(7),
                                //NumVoie = reader.IsDBNull(8) ? null : reader.GetString(8),
                                Btqc = reader.IsDBNull(9) ? null : reader.GetString(9),
                                TypeVoie = reader.IsDBNull(10) ? null : reader.GetString(10),
                                TelephoneAutre = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Fax = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Batesc = reader.IsDBNull(13) ? null : reader.GetString(13),
                                Description = reader.IsDBNull(14) ? null : reader.GetString(14),
                                Nom = reader.IsDBNull(15) ? null : reader.GetString(15),
                                ParDefaut = reader.IsDBNull(16) ? (bool?)null : reader.GetBoolean(16),
                                Cp = reader.IsDBNull(17) ? null : reader.GetString(17),
                                Commune = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Bureau = reader.IsDBNull(19) ? null : reader.GetString(19)
                            });
                        }
                    }
                }
            }

            return Result<List<ClientAddressDetailsDto>>.Success(resultList);
        }

    }
}