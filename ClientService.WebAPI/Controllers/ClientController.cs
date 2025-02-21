using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Infrastructure.Dtos;
using ClientService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;


        public ClientController(IClientService clientService,ILogger<ClientController> logger)
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

        [HttpGet("VentesNationales/{clientId}")]
        public async Task<ActionResult<List<VentesNationales>>> GetVentesNationales(int clientId)
        {
            try
            {
                _logger.LogInformation("Retrieving invoices for client {ClientId}", clientId);

                var factures = await _clientService.GetVentesNationales(clientId);

                if (factures == null || !factures.Any())
                {
                    _logger.LogWarning("No invoices found for client {ClientId}", clientId);
                    return NotFound($"No invoices found for client ID: {clientId}");
                }

                return Ok(factures);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoices for client {ClientId}", clientId);
                return StatusCode(500, "An error occurred while retrieving the invoices");
            }
        }

        


        [HttpGet("clients/{clientId}/addresses")]
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

        // Get Chiffre d'affaire par client
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
    }
}
