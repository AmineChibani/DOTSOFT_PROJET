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
            try
            {
                // This LINQ query exactly mirrors your SQL query
                var addresses = await (
                    from ca in _appcontext.ClientAdresses
                    join pta in _appcontext.TypeAdresses on
                        ca.AdresseTypeId equals pta.Id
                    join pp in _appcontext.Pays on
                        (ca.PaysId ?? 65) equals pp.PaysId into ppJoin
                    from pp in ppJoin.DefaultIfEmpty()
                    join lpp in _appcontext.Language_Param_Pays on
                        new { IdPays = pp.PaysId, Code = (int?)1 } equals
                        new { IdPays = lpp.Id_Pays, Code = lpp.Code } into lppJoin
                    from lpp in lppJoin.DefaultIfEmpty()
                    join pcp in _appcontext.ParamCodePostals on
                        ca.CpId equals pcp.CPId into pcpJoin
                    from pcp in pcpJoin.DefaultIfEmpty()
                    where ca.ClientId == clientId &&
                          (ca.AdresseTypeId == 1 || ca.AdresseTypeId == 2)
                    orderby ca.AdresseTypeId
                    select new ClientAddressDetailsDto
                    {
                        IdClient = ca.ClientId,
                        IdTypeAdresse = ca.AdresseTypeId,
                        Adresse1 = ca.Adresse1,
                        Adresse2 = ca.Adresse2,
                        IdCp = ca.CpId,
                        IdPays = ca.PaysId ?? 65, // COALESCE equivalent
                        Telephone = ca.PhoneNumber,
                        Portable = ca.CellPhone,
                        NumVoie = ca.NumVoie,
                        Btqc = ca.Btqc,
                        TypeVoie = ca.TypeVoie,
                        TelephoneAutre = ca.TelephoneAutre,
                        Fax = ca.Fax,
                        Batesc = ca.Batesc,
                        Description = pta.Description,
                        Nom = lpp != null ? lpp.Nom : pp.Libelle, // COALESCE equivalent
                        //ParDefaut = pp.ParDefaut,
                        Cp = pcp.CP,
                        Commune = pcp.Commune,
                        //Bureau = pcp.Bureau
                    }
                ).ToListAsync();

                if (!addresses.Any())
                {
                    return Result<List<ClientAddressDetailsDto>>.Failure($"No addresses found for client ID {clientId}");
                }

                return Result<List<ClientAddressDetailsDto>>.Success(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching addresses for client ID {clientId}: {ex.Message}", ex);
                return Result<List<ClientAddressDetailsDto>>.Failure(ex.Message);
            }
        }
    }
}