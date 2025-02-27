using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Common.Pagination;
using ClientService.Core.Dtos;
using ClientService.Core.Dtos.ClientService.Core.Dtos;
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

        public async Task<Result<PagedResult<ClientDto>>> GetClientsAsync(ClientFilter filter)
        {
            if (filter == null)
                return Result<PagedResult<ClientDto>>.Failure("Filter cannot be null.");

            try
            {
                var query = _appcontext.Clients.AsNoTracking();

                // Apply filters
                foreach (var clause in filter.ToWhereClauses())
                {
                    query = query.Where(clause);
                }

                // Apply sorting
                query = filter.ApplySorting(query, filter.OrderByColumn, filter.SortDirection);

                // Get total count before pagination
                int totalCount = await query.CountAsync();

                // Apply pagination and projection
                var clients = await query
                 .Skip((filter.PageNumber - 1) * filter.PageSize)
                 .Take(filter.PageSize)
                 .Select(client => new ClientDto
                 {
                     ClientId = client.ClientId,
                     Email = client.Mail,
                     FirstName = client.Prenom,
                     LastName = client.Nom,
                     ClientAdresses = client.ClientAdresses.Select(addr => new ClientAdressesDto
                     {
                         Address = addr.Adresse1,
                         City = addr.ParamCodePostal.Commune,
                         PostalCode = addr.ParamCodePostal.CP,
                         Country = addr.Pays.Libelle
                     }).ToList()
                 })
                 .ToListAsync();


                var pagedResult = new PagedResult<ClientDto>(filter.PageNumber, filter.PageSize, totalCount, totalCount)
                {
                    Results = clients
                };

                return Result<PagedResult<ClientDto>>.Success(pagedResult);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<ClientDto>>.Failure($"An error occurred: {ex.Message}");
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


        //méthode pour récupére tous les factures clients qui a fait spécifique  client
        public async Task<Result<IEnumerable<CAResult>>> GetCAAsync(CARequest request)
        {
            try
            {
                if (request.IdClient <= 0)
                {
                    return Result<IEnumerable<CAResult>>.Failure("Invalid Client ID");
                }

                // Create output parameter for the cursor
                var outputParameter = new OracleParameter
                {
                    ParameterName = "p_Result",
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor
                };

                // Execute stored procedure with OracleParameter to handle IN and OUT parameters
                var result = await _appcontext.Set<CAResult>()
                    .FromSqlRaw("BEGIN DOTSOFT.GetCA(:p_IdClient, :p_Result); END;",
                                new OracleParameter(":p_IdClient", request.IdClient),
                                outputParameter)
                    .ToListAsync();

                return Result<IEnumerable<CAResult>>.Success(result.OrderByDescending(x => x.Fdate).ToList());
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "Oracle error occurred while getting CA data for client {ClientId}", request.IdClient);
                return Result<IEnumerable<CAResult>>.Failure($"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting CA data for client {ClientId}", request.IdClient);
                return Result<IEnumerable<CAResult>>.Failure($"Error: {ex.Message}");
            }
        }

        // pour récuperer les ventes nationales d'un client 
        public async Task<Result<List<VenteResult>>> GetVentesNationalesAsync(VenteRequest request)
        {
            try
            {
                var idClientParam = new OracleParameter("p_id_client", OracleDbType.Int32)
                {
                    Value = request.IdClient,
                    Direction = ParameterDirection.Input
                };

                var abandonneeParam = new OracleParameter("p_abandonnee", OracleDbType.Int32)
                {
                    Value = request.Abandonnee,
                    Direction = ParameterDirection.Input
                };

                var idStructureParam = new OracleParameter("p_id_structure", OracleDbType.Int32)
                {
                    Value = request.IdStructure ?? (object)DBNull.Value,
                    Direction = ParameterDirection.Input,
                    IsNullable = true
                };

                var resultParam = new OracleParameter
                {
                    ParameterName = "result_cursor",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                var resultList = await _appcontext.ventesNationales
                    .FromSqlRaw("BEGIN DOTSOFT.GET_Ventes_Nationales(:p_id_client, :p_abandonnee, :p_id_structure, :result_cursor); END;",
                                idClientParam, abandonneeParam, idStructureParam, resultParam)
                    .ToListAsync();

                return Result<List<VenteResult>>.Success(resultList);
            }
            catch (OracleException ex)
            {
                return Result<List<VenteResult>>.Failure($"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<List<VenteResult>>.Failure($"Error: {ex.Message}");
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

        public async Task<Result<GetOptinBaseDto>> GetOptin(int clientId, int idStructure)
        {
            try
            {
                // Get the client
                var client = await _appcontext.Clients
                    .FirstOrDefaultAsync(c => c.ClientId == clientId);

                if (client == null)
                {
                    return Result<GetOptinBaseDto>.Failure("Client not found");
                }

                // Get the address
                var Adresse = await _appcontext.ClientAdresseComplement
                    .FirstOrDefaultAsync(a => a.ClientId == clientId && a.AdresseTypeId == 1);
                if (Adresse == null)
                {
                    return Result<GetOptinBaseDto>.Failure("Adress not found");
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
                    return Result<GetOptinBaseDto>.Failure("An error occured while getting affiliations preferences: " + ex.Message);
                }

                return Result<GetOptinBaseDto>.Success(client.ToClientOptinDto(Adresse, Affiliation));
            }
            catch (Exception ex)
            {
                return Result<GetOptinBaseDto>.Failure("An error occured while getting communication preferences: " + ex.Message);
            }
        }
        public async Task<Result<List<AvoirResult>>> GetAvoirData(int clientId)
        {
            try
            {          
                var clientIdParam = new OracleParameter("ClientId", OracleDbType.Int32)
                {
                    Value = clientId,
                    Direction = ParameterDirection.Input
                };

                var resultParam = new OracleParameter
                {
                    ParameterName = "result",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                var resultList = await _appcontext.AvoirResults
                    .FromSqlRaw("BEGIN DOTSOFT.GetAvoir(:ClientId, :result); END;", clientIdParam, resultParam)
                    .ToListAsync();

                return Result<List<AvoirResult>>.Success(resultList);
            }
            catch (OracleException ex)
            {
                return Result<List<AvoirResult>>.Failure($"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<List<AvoirResult>>.Failure($"Error: {ex.Message}");
            }
        }
    }
}