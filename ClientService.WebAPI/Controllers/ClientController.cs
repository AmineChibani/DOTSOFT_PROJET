using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;

        }


        [HttpGet("Getpays")]
        public async Task<IActionResult> GetAllPays()
        {
            var result = await _clientService.GetAllPays();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetClientById([FromRoute] int clientId)
        {
            var client = await _clientService.GetClientById(clientId);
            if (!client.IsSuccess)
            {
                return NotFound(client.Error);
            }
            return Ok(client.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientService.GetClients();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] AddClientDto newClientDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Mapp Dto to  DbClient entity
            DbClient newClient = new DbClient
            {
                Prenom = newClientDto.Prenom,
                Nom = newClientDto.Nom,
                Nom2 = newClientDto.Nom2,
                TitreId = newClientDto.TitreId,
                BirthdayDate = newClientDto.BirthdayDate,
                Mail = newClientDto.Mail,
                Fdate = newClientDto.Fdate,
                PointsNumber = newClientDto.PointsNumber,
                IdTypeClient = newClientDto.IdTypeClient,
                Particulier = newClientDto.Particulier,
                StructureId = newClientDto.StructureId,
                PourMailing = newClientDto.PourMailing,
                Mailing = newClientDto.Mailing,
                OkPourMailing = newClientDto.OkPourMailing,
                PremierAchatPlus = newClientDto.PremierAchatPlus,
                EmployeId = newClientDto.EmployeId,
                EmployeIdModification = newClientDto.EmployeIdModification,
                FDateModification = newClientDto.FDateModification,
                Commentaire = newClientDto.Commentaire,
                Interets = newClientDto.Interets,
                CodeExterne = newClientDto.CodeExterne,
                NumClient = newClientDto.NumClient,
                OkPourSms = newClientDto.OkPourSms,
                OkPourSmsAff = newClientDto.OkPourSmsAff,
                OkPourMailingPartner = newClientDto.OkPourSmsAff,
                OkPourSmsPartner = newClientDto.OkPourSmsAff,
                OkPourMailingAff = newClientDto.OkPourSmsAff,
                Eticket = newClientDto.Eticket,
                WebType = newClientDto.WebType,
                RaisonSociale = newClientDto.RaisonSociale,
                LivrRaisonSociale = newClientDto.LivrRaisonSociale
            };

            var Client = await _clientService.AddClient(newClient);
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Nom }, newClient);
        }

        [HttpGet("clients/addresses/{clientId}")]
        public async Task<IActionResult> GetClientAddresses(int clientId)
        {
            var result = await _clientService.GetAddressesByClientId(clientId);
            if (!result.IsSuccess)
            {
                return result.Error.Contains("not found") ?
                    NotFound(result.Error) :
                    BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("duplicate/{clientId}/{AdressTypeId}")]
        public async Task<IActionResult> DuplicateAdresse([FromRoute] int clientId, [FromRoute] int AdressTypeId)
        {
            var result = await _clientService.Duplicate(clientId, AdressTypeId);
            if (!result.IsSuccess)
            {
                return result.Error.Contains("not found") ?
                NotFound(result.Error) :
                    BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("Csps")]
        public async Task<IActionResult> GetCsps()
        {
            var result = await _clientService.GetCSPs();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }


        [HttpGet("CA/{clientId}")]
        public async Task<ActionResult<IEnumerable<CAResult>>> GetCA(int clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ArgumentException("Invalid Client ID");
                }

                // Création d'un objet CARequest avec le seul IdClient
                var request = new CARequest
                {
                    IdClient = clientId
                };

                var results = await _clientService.GetCAAsync(request);
                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing CA request for client {ClientId}", clientId);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("ventes-nationales")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VenteResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVentesNational(
    [FromQuery] VenteRequest request)
        {
            try
            {
                // Validate the request input
                if (request == null)
                {
                    return BadRequest("Request cannot be null.");
                }

                if (request.IdClient <= 0)
                {
                    return BadRequest("Invalid Client ID.");
                }

                // Call the service to get the national sales
                var results = await _clientService.GetVentesNationalesAsync(request);

                // If no results, return NotFound
                if (results == null || !results.Any())
                {
                    return NotFound($"No national sales found for client {request.IdClient}.");
                }

                // Return the results with a 200 OK response
                return Ok(results);
            }
            catch (ArgumentException ex)
            {
                // Catch validation errors (like invalid input) and return BadRequest
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other errors and log them, return a 500 status
                _logger.LogError(ex, "Error occurred while processing national sales data for client {ClientId}", request.IdClient);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

    }
}
