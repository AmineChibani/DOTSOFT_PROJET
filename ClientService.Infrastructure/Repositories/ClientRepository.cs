<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Core.Specifications.Clients;
using ClientService.Infrastructure.Data;
using Microsoft.Data.SqlClient;
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


        public Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId)
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

        //méthode pour récupére tous les factures clients qui a fait spécifique le client
        public async Task<IEnumerable<CAResult>> GetCAAsync(CARequest request)
        {
            try
            {
                if (request.IdClient <= 0)
                {
                    throw new ArgumentException("Invalid Client ID");
                }

                // Créer un paramètre pour le curseur de sortie
                var outputParameter = new OracleParameter
                {
                    ParameterName = "p_Result",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor
                };

                // Exécuter la procédure stockée avec OracleParameter pour gérer les paramètres IN et OUT
                var result = await _appcontext.Set<CAResult>()
                    .FromSqlRaw("BEGIN DOTSOFT.GetCA(:p_IdClient, :p_Result); END;",
                                new OracleParameter(":p_IdClient", request.IdClient),
                                outputParameter)
                    .ToListAsync();

                return result.OrderByDescending(x => x.Fdate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting CA data for client {ClientId}", request.IdClient);
                throw;
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

        // pour récuperer les ventes nationales d'un client 
        public async Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request)
        {
            try
            {
                // Previous parameter setup remains the same
                var parameters = new[]
                {
                new OracleParameter("p_id_client", OracleDbType.Int32) { Value = request.IdClient },
                new OracleParameter("p_abandonnee", OracleDbType.Int32) { Value = request.Abandonnee },
                new OracleParameter("p_id_structure", OracleDbType.Int32)
                {
                    Value = request.IdStructure.HasValue ? (object)request.IdStructure.Value : DBNull.Value,
                    IsNullable = true
                },
                new OracleParameter
                    {
                        ParameterName = "result_cursor",
                        Direction = ParameterDirection.Output,
                        OracleDbType = OracleDbType.RefCursor
                    }
                };

                // Make sure the DbContext is configured properly
                _appcontext.Database.SetCommandTimeout(120); // Optional: increase timeout if needed

                var sql = @"
                            BEGIN 
                                DOTSOFT.GET_Ventes_Nationales(
                                    :p_id_client, 
                                    :p_abandonnee, 
                                    :p_id_structure, 
                                    :result_cursor
                                ); 
                            END;";

                // Use explicit mapping configuration
                var results = await _appcontext.Set<VenteResult>()
                    .FromSqlRaw(sql, parameters)
                    .AsNoTracking()
                    .ToListAsync();

                return results.OrderByDescending(x => x.FDate)
                             .ThenByDescending(x => x.Num_Facture)
                             .ThenBy(x => x.Id_Structure);
            }
            catch (OracleException oex)
            {
                _logger.LogError(oex,
                    "Oracle error getting national sales data. ErrorCode: {ErrorCode}, Message: {Message}",
                    oex.ErrorCode, oex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error getting national sales data. Client: {ClientId}, Abandonnee: {Abandonnee}, Structure: {StructureId}",
                    request.IdClient, request.Abandonnee, request.IdStructure);
                throw;
            }
        }
    }
=======
﻿using System;
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
            try
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
                                    IdTypeAdresse = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                                    Adresse1 = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Adresse2 = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    IdCp = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                                    IdPays = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                                    Telephone = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    Portable = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    NumVoie = reader.IsDBNull(8) ? null : reader.GetInt32(8),
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
            catch(Exception ex) {
                return Result<List<ClientAddressDetailsDto>>.Failure("Error occured while trying to get Client's adress: "+ ex.Message);
            }
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> develop

        public async Task<Result<int>> Duplicate(int idClient, int adresseTypeId)
        {
            var oldClientAdresse = await _appcontext.ClientAdresses
                .FirstOrDefaultAsync(x => x.ClientId == idClient && x.AdresseTypeId == adresseTypeId);

            if (oldClientAdresse == null)
                return Result<int>.Failure("Client address not found.");

            var existingAdresse = await _appcontext.ClientAdresses
                .FirstOrDefaultAsync(x => x.ClientId == idClient && x.AdresseTypeId == 2);

            if (existingAdresse == null)
            {
                var newAdresse = new DbClientAdresse
                {
                    ClientId = oldClientAdresse.ClientId,
                    AdresseTypeId = 2,
                    ParamCodePostal = oldClientAdresse.ParamCodePostal,
                    Adresse1 = oldClientAdresse.Adresse1,
                    Adresse2 = oldClientAdresse.Adresse2,
                    Abandon = oldClientAdresse.Abandon,
                    VilleEtranger = oldClientAdresse.VilleEtranger,
                    CpEtranger = oldClientAdresse.CpEtranger,
                    Batesc = oldClientAdresse.Batesc,
                    CellPhone = oldClientAdresse.CellPhone,
                    PhoneNumber = oldClientAdresse.PhoneNumber,
                    TypeVoie = oldClientAdresse.TypeVoie,
                    PaysId = oldClientAdresse.PaysId,
                    CpId = oldClientAdresse.CpId,
                    NumVoie = oldClientAdresse.NumVoie,
                    Btqc = oldClientAdresse.Btqc,
                    TelephoneAutre = oldClientAdresse.TelephoneAutre,
                    Fax = oldClientAdresse.Fax,
                    Pays = oldClientAdresse.Pays
                };

                _appcontext.ClientAdresses.Add(newAdresse);
            }
            else
            {
                existingAdresse.Adresse1 = oldClientAdresse.Adresse1;
                existingAdresse.ParamCodePostal = oldClientAdresse.ParamCodePostal;
                existingAdresse.Adresse2 = oldClientAdresse.Adresse2;
                existingAdresse.Abandon = oldClientAdresse.Abandon;
                existingAdresse.VilleEtranger = oldClientAdresse.VilleEtranger;
                existingAdresse.CpEtranger = oldClientAdresse.CpEtranger;
                existingAdresse.Batesc = oldClientAdresse.Batesc;
                existingAdresse.CellPhone = oldClientAdresse.CellPhone;
                existingAdresse.PhoneNumber = oldClientAdresse.PhoneNumber;
                existingAdresse.TypeVoie = oldClientAdresse.TypeVoie;
                existingAdresse.PaysId = oldClientAdresse.PaysId;
                existingAdresse.NumVoie = oldClientAdresse.NumVoie;
                existingAdresse.Btqc = oldClientAdresse.Btqc;
                existingAdresse.TelephoneAutre = oldClientAdresse.TelephoneAutre;
                existingAdresse.Fax = oldClientAdresse.Fax;
                existingAdresse.Pays = oldClientAdresse.Pays;
            }

            await _appcontext.SaveChangesAsync();

            return Result<int>.Success(oldClientAdresse.ClientId);
        }

<<<<<<< HEAD
>>>>>>> cefeeb1fc6f3e47c423f66bbbe9bbadd67e7cec3
=======
>>>>>>> develop
    }
>>>>>>> cefeeb1fc6f3e47c423f66bbbe9bbadd67e7cec3
}