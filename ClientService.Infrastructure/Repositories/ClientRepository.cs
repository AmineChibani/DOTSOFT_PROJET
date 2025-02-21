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

        public async Task<IEnumerable<CAResult>> GetCAAsync(CARequest request)
        {
            var query = from f in _appcontext.ClientFactures
                        join fl in _appcontext.ClientFatureLignes
                        on f.IdFactureC equals fl.IdFactureC
                        where f.IdClient == request.IdClient
                        && f.IdStructure == request.IdStructure
                        && f.Annulation == 0
                        && f.Avoir == 0
                        && (request.All == 1 ||
                            (request.StartDate.HasValue &&
                             request.EndDate.HasValue &&
                             f.Fdate >= request.StartDate.Value &&
                             f.Fdate <= request.EndDate.Value))
                        group new { f, fl } by new
                        {
                            f.IdFactureC,
                            f.Fdate,
                            f.NumFacture,
                            f.TypeFacture
                        } into g
                        select new CAResult
                        {
                            Fdate = g.Key.Fdate,
                            IdFactureC = g.Key.IdFactureC,
                            NumFacture = g.Key.NumFacture.ToString(),
                            TypeFacture = g.Key.TypeFacture,
                            Cattc = g.Sum(x => x.fl.Montant * x.fl.Quantite),
                            Achat = g.Sum(x => x.fl.MontantAchat * x.fl.Quantite),
                            Caht = g.Sum(x => x.fl.Montant / (1 + (x.fl.MontantTva / 100m)))
                        };

            return await query.OrderByDescending(x => x.Fdate).ToListAsync();
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

        public Task<List<CAResult>> GetClientCA(int clientId, int structureId)
        {
            throw new NotImplementedException();
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

        public async Task<List<VentesNationales>> GetVentesNationales(int clientId)
        {
            var parameter = new OracleParameter("p_client_id", OracleDbType.Int32)
            {
                Value = clientId
            };

            return await _appcontext.ventesNationales
                .FromSqlRaw("BEGIN DOTSOFT.GET_VENTES_NATIONALE(:p_client_id, :p_resultset); END;",
                    parameter)
                .ToListAsync();
        }
    }
}