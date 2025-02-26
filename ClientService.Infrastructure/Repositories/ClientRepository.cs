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
using ClientService.Core.Mappers;
using ClientService.Core.Specifications.Clients;
using ClientService.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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
                // Create parameters for the stored procedure
                var clientIdParam = new OracleParameter("ClientId", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = clientId
                };
                var resultParam = new OracleParameter("ResultCursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };

                var result = await _appcontext.Set<ClientAddressDetailsDto>()
                    .FromSqlRaw("BEGIN DOTSOFT.GetClientAddresses(:ClientId, :ResultCursor); END;",
                        clientIdParam, resultParam)
                    .ToListAsync();

                return Result<List<ClientAddressDetailsDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<ClientAddressDetailsDto>>.Failure("Error occured while trying to get Client's adress: " + ex.Message);
            }
        }

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

        public async Task<Result<List<DbParamCategSocioProf>>> GetCSPs()
        {
            try
            {
                List<DbParamCategSocioProf> csps = await _appcontext.ParamCategSocioProfS
                                        .Where(x => x.LanguageParamCategSocioPro.Code == 1)
                                        .ToListAsync();

                return Result<List<DbParamCategSocioProf>>.Success(csps);
            }
            catch (Exception ex)
            {
                return Result<List<DbParamCategSocioProf>>.Failure("An error occured while retreiving the Csps" + ex.Message);
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

        // pour récuperer les ventes nationales d'un client 
        public async Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request)
        {
            try
            {
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

                using var connection = _appcontext.Database.GetDbConnection() as OracleConnection;
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                                        BEGIN 
                                            DOTSOFT.GET_Ventes_Nationales(
                                                :p_id_client, 
                                                :p_abandonnee, 
                                                :p_id_structure, 
                                                :result_cursor
                                            ); 
                                        END;";
                command.Parameters.AddRange(parameters);

                var results = new List<VenteResult>();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    results.Add(new VenteResult
                    {

                        Nom = reader["nom"] as string,
                        Id_Structure = Convert.ToInt32(reader["id_structure"]),
                        Type_Avoir = reader["type_avoir"] as string ?? "None",
                        Avoir = reader["avoir"] != DBNull.Value ? Convert.ToInt32(reader["avoir"]) : (int?)null,
                        Num_Facture = reader["num_facture"] != DBNull.Value ? Convert.ToInt64(reader["num_facture"]) : (long?)null,
                        Id_Facture = Convert.ToInt32(reader["id_facture"]),
                        FDate = reader["fdate"] as DateTime?,
                        Montant_Facture = reader["montant_facture"] as decimal?,
                        Quantite = reader["quantite"] as decimal?,
                        Montant_Produit = reader["montant_produit"] as decimal?,
                        Nom_Produit = reader["nom_produit"] as string,
                        Montant_Achat = reader["montant_achat"] as decimal?,
                        Montant_Tva = reader["montant_tva"] as decimal?,
                        Id_Client = Convert.ToInt32(reader["id_client"]),
                        Id_Produit = Convert.ToInt32(reader["id_produit"]),
                        Code_Reference = reader["code_reference"] as string,
                        Sans_Marge = Convert.ToBoolean(reader["sans_marge"]),
                        Type_Facture = reader["type_facture"] as string
                    });
                }

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

        public async Task<Result<List<EnCours>>> GetEnCoursAsync(int idClient, int idStructure)
        {
            try
            {
                // Create parameters for the stored procedure
                var idClientParam = new OracleParameter("p_id_client", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = idClient
                };

                var idStructureParam = new OracleParameter("p_id_structure", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = idStructure
                };

                var resultParam = new OracleParameter("p_recordset", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };

                var result = await _appcontext.Set<EnCours>()
                    .FromSqlRaw("BEGIN DOTSOFT.GET_EN_COURS(:p_id_client, :p_id_structure, :p_recordset); END;",
                        idClientParam, idStructureParam, resultParam)
                    .ToListAsync();

                return Result<List<EnCours>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<List<EnCours>>.Failure("Error getting Encours : " + ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteClient(int idClient)
        {
            try
            {
                var client = await _appcontext.Clients.FirstOrDefaultAsync(x => x.ClientId == idClient);
                if (client == null)
                {
                    return Result<bool>.Failure("Client not found");
                }
                _appcontext.Clients.Remove(client);
                _appcontext.SaveChanges();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Error deleting client: " + ex.Message);
            }

        }

        public async Task<Result<CommunicationPreferencesBaseDto>> GetClientCommunicationPreferencesAsync(int clientId, int idStructure)
        {
            try
            {
                // Get the client
                var client = await _appcontext.Clients
                    .FirstOrDefaultAsync(c => c.ClientId == clientId);

                if (client == null)
                {
                    return Result<CommunicationPreferencesBaseDto>.Failure("Client not found");
                }

                // Get the address
                var Adresse = await _appcontext.ClientAdresseComplement
                    .FirstOrDefaultAsync(a => a.ClientId == clientId && a.AdresseTypeId == 1);
                if (Adresse == null)
                {
                    return Result<CommunicationPreferencesBaseDto>.Failure("Adress not found");
                }

                // Check for affiliation
                bool Affiliation = false;
                try
                {
                    Affiliation = await _appcontext.CritereBoutiqueStructure
                        .Join(_appcontext.Structure,
                            cbs => cbs.IdStructure,
                            s => s.IdStructure,
                            (cbs, s) => new { CritereBoutique = cbs, Structure = s })
                        .AnyAsync(x =>
                            x.CritereBoutique.IdStructure == idStructure &&
                            x.CritereBoutique.IdCritere == 4 &&
                            x.CritereBoutique.Valeur.ToLower().Trim().StartsWith("affili") &&
                            (x.Structure.id_pays == 65 || x.Structure.id_pays == 59));
                }
                catch (Exception ex)
                {
                    return Result<CommunicationPreferencesBaseDto>.Failure("An error occured while getting affiliations preferences: " + ex.Message);
                }

                return Result<CommunicationPreferencesBaseDto>.Success(client.ToClientCommunicationPreferencesDto(Adresse, Affiliation));
            }
            catch (Exception ex)
            {
                return Result<CommunicationPreferencesBaseDto>.Failure("An error occured while getting communication preferences: " + ex.Message);
            }
        }
        public async Task<Result<List<AvoirResult>>> GetAvoirData(int clientId)
        {
            var query = @"
    BEGIN 
        DOTSOFT.GetAvoir(:ClientId, :result); 
    END;";

            try
            {
                // Create the command and parameters
                using (var command = _appcontext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    // Set up input parameter for clientId
                    var clientIdParameter = new OracleParameter("ClientId", OracleDbType.Int32)
                    {
                        Value = clientId,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(clientIdParameter);

                    // Set up the output parameter for the cursor
                    var resultParameter = new OracleParameter("result", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParameter);

                    // Open the connection if it's not already open
                    if (command.Connection.State != ConnectionState.Open)
                        await command.Connection.OpenAsync();

                    // Execute the command
                    await command.ExecuteNonQueryAsync();

                    // Read from the RefCursor
                    using (var reader = ((OracleRefCursor)resultParameter.Value).GetDataReader())
                    {
                        var resultList = new List<AvoirResult>();

                        // Create a case-insensitive dictionary to map columns
                        var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columnMap[reader.GetName(i)] = i;
                        }

                        // Debug: Print all available columns from the database
                        Console.WriteLine("Available columns in database result:");
                        foreach (var col in columnMap.Keys)
                        {
                            Console.WriteLine($"- {col}");
                        }

                        // Column name mappings (database column name to property name)
                        var columnMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    // Map actual DB column names to model property names
                    { "ID_BA", "ID_BA" },
                    { "DATE_CREATION", "FDATE" }, // Special case: DATE_CREATION maps to FDATE
                    // Add other mappings as needed
                };

                        // Map the data to AvoirResult with case-insensitive lookups
                        while (await reader.ReadAsync())
                        {
                            var resultItem = new AvoirResult();

                            // Helper method for safe value retrieval with case-insensitive column names
                            T GetSafeValue<T>(string columnName, T defaultValue = default)
                            {
                                // Try the mapped column name first if it exists
                                string actualColumnName = columnName;
                                if (columnMappings.ContainsKey(columnName))
                                    actualColumnName = columnMappings[columnName];

                                // Find column in a case-insensitive way
                                var matchingColumn = columnMap.Keys.FirstOrDefault(k =>
                                    string.Equals(k, actualColumnName, StringComparison.OrdinalIgnoreCase));

                                if (matchingColumn == null)
                                    return defaultValue;

                                var ordinal = columnMap[matchingColumn];
                                if (reader.IsDBNull(ordinal))
                                    return defaultValue;

                                try
                                {
                                    var value = reader.GetValue(ordinal);

                                    if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                                        return (T)(object)Convert.ToInt32(value);
                                    else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
                                        return (T)(object)Convert.ToDecimal(value);
                                    else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                                        return (T)(object)Convert.ToDateTime(value);
                                    else if (typeof(T) == typeof(string))
                                        return (T)(object)value.ToString();
                                    else if (typeof(T) == typeof(long) || typeof(T) == typeof(long?))
                                        return (T)(object)Convert.ToInt64(value);

                                    else
                                        return (T)value;
                                }
                                catch
                                {
                                    return defaultValue;
                                }
                            }

                            // Set properties directly
                            resultItem.ID_BA = GetSafeValue<int>("ID_BA"); // The actual ID_BA value
                            resultItem.ID_STRUCTURE = GetSafeValue<int?>("ID_STRUCTURE");
                            resultItem.ID_EMPLOYE = GetSafeValue<int?>("ID_EMPLOYE");
                            resultItem.FDATE = GetSafeValue<DateTime?>("DATE_CREATION"); // Map DATE_CREATION to FDATE
                            resultItem.DATE_DEBUT = GetSafeValue<DateTime?>("DATE_DEBUT");
                            resultItem.DATE_FIN = GetSafeValue<DateTime?>("DATE_FIN");
                            resultItem.ID_CLIENT = GetSafeValue<int?>("ID_CLIENT");
                            resultItem.CODE = GetSafeValue<string>("CODE");
                            resultItem.ANNULE = GetSafeValue<int?>("ANNULE");
                            resultItem.MONTANT_BA_INITIAL = GetSafeValue<decimal?>("MONTANT_BA_INITIAL");
                            resultItem.MONTANT_BA_VALIDE = GetSafeValue<decimal?>("MONTANT_BA_VALIDE");
                            resultItem.MONTANT_BA_ENCOURS = GetSafeValue<decimal?>("MONTANT_BA_ENCOURS");
                            resultItem.TYPE_BA = GetSafeValue<int?>("TYPE_BA");
                            resultItem.DATE_ANNULATION = GetSafeValue<DateTime?>("DATE_ANNULATION");
                            resultItem.TYPE_ANNULATION = GetSafeValue<int?>("TYPE_ANNULATION");
                            resultItem.ID_EMPLOYE_ANNULE = GetSafeValue<int?>("ID_EMPLOYE_ANNULE");
                            resultItem.TYPE_REGLEMENT_CMD = GetSafeValue<int?>("TYPE_REGLEMENT_CMD");
                            resultItem.ID_COMMANDEC = GetSafeValue<int?>("ID_COMMANDEC");
                            resultItem.VALIDITE_BA_MOIS = GetSafeValue<int?>("VALIDITE_BA_MOIS");
                            resultItem.ID_FACTUREC = GetSafeValue<int?>("ID_FACTUREC");
                            // Fix for TAUX_TVA - try both column names
                            resultItem.TAUX_TVA = GetSafeValue<decimal?>("TAUX_TVA") ?? GetSafeValue<decimal?>("AUX_TVA");
                            resultItem.ID_DISTRIB = GetSafeValue<int?>("ID_DISTRIB");
                            resultItem.CODE_BARRE_COMPLET = GetSafeValue<string>("CODE_BARRE_COMPLET");
                            resultItem.DESTINATAIRE = GetSafeValue<string>("DESTINATAIRE");
                            resultItem.MESSAGE = GetSafeValue<string>("MESSAGE");
                            resultItem.ID_DEVISE = GetSafeValue<int?>("ID_DEVISE");
                            resultItem.montant_BA_restant = GetSafeValue<decimal?>("MONTANT_BA_RESTANT");
                            resultItem.ID_TICKET_CAISSE = GetSafeValue<long?>("ID_TICKET_CAISSE");
                            resultItem.num_commande = GetSafeValue<int?>("NUM_COMMANDE");

                            resultList.Add(resultItem);
                        }

                        return Result<List<AvoirResult>>.Success(resultList);
                    }
                }
            }
            catch (OracleException ex)
            {
                return Result<List<AvoirResult>>.Failure($"An Oracle error occurred while returning Avoirs: {ex.Message}");
            }
            catch (InvalidCastException ex)
            {
                return Result<List<AvoirResult>>.Failure($"Invalid cast occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<List<AvoirResult>>.Failure($"An error occurred: {ex.Message}");
            }
        }



    }

}