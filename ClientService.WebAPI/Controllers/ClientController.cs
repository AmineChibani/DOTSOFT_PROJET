using System.Net.Sockets;
using ClientService.Core.Common;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.Core.Specifications.Clients;
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

        [HttpGet("GetClientById/{clientId}")]
        public async Task<IActionResult> GetClientById([FromRoute] int clientId)
        {
            var client = await _clientService.GetClientById(clientId);
            if (!client.IsSuccess)
            {
                return NotFound(client.Error);
            }
            return Ok(client.Value);
        }

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients([FromQuery] ClientFilter filter)
        {
            var result = await _clientService.GetClientsAsync(filter);
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientRequest newClient)
        {
            int clientId = await _clientService.Create(newClient);
            return StatusCode(201, newClient);
        }

        [HttpGet("clients/GetAddresses/{clientId}/{codelibelle}")]
        public async Task<IActionResult> GetClientAddresses([FromRoute] int clientId, [FromRoute] int codelibelle)
        {
            var result = await _clientService.GetAddressesByClientId(clientId, codelibelle);
            if (!result.IsSuccess)
            {
                return result.Error!.Contains("not found") ?
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
                return result.Error!.Contains("not found") ?
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
        public async Task<ActionResult<CAResult>> GetCA(int clientId)
        {
            var CaRequest = new CARequest
            {
                IdClient = clientId,
            };

            var result = await _clientService.GetCAAsync(CaRequest);
            if (!result.IsSuccess)
            {
                return result.Error!.Contains("not found") ?
                  NotFound(result.Error) :
                  BadRequest(result.Error);
            }
            return Ok(result.Value);
        }


        [HttpGet("ventes-nationales")]
        public async Task<IActionResult> GetVentesNational([FromQuery] VenteRequest request)
        {
            var result = await _clientService.GetVentesNationalesAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }


        [HttpGet("{idClient}/{idStructure}/Encours")]
        public async Task<IActionResult> GetEncours([FromRoute] int idClient, [FromRoute] int idStructure)
        {
            var result = await _clientService.GetEnCoursAsync(idClient, idStructure);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        //[HttpDelete("DeleteClient/{idClient}")]
        //public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        //{
        //    var result = await _clientService.DeleteClient(idClient);
        //    if (!result.IsSuccess)
        //    {
        //        return result.Error!.Contains("not found") ?
        //           NotFound(result.Error) :
        //           BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}


        [HttpGet("GetOptin/{idClient}/{idStructure}")]
        public async Task<IActionResult> GetOptin([FromRoute] int idClient, [FromRoute] int idStructure)
        {
            var result = await _clientService.GetOptin(idClient, idStructure);
            if (!result.IsSuccess)
            {
                return result.Error!.Contains("not found") ?
                   NotFound(result.Error) :
                   BadRequest(result.Error);
            }
            return Ok(result.Value);
        }


        [HttpGet("{idClient}/Avoirs")]
        public async Task<IActionResult> GetAvoirs([FromRoute] int idClient)
        {
            var result = await _clientService.GetAvoirData(idClient);
            if (!result.IsSuccess)
            {
                return result.Error!.Contains("not found") ?
                                  NotFound(result.Error) :
                                  BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("GetHistoVentes/{clientId}")]
        public async Task<IActionResult> GetHistoVentes(int clientId)
        {
            try
            {
                var salesHistory = await _clientService.GetHistoVentes(clientId);
                return Ok(salesHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("regions")]
        public async Task<IActionResult> GetRegions([FromQuery] int? paysId)
        {
            var regions = await _clientService.GetRegions(paysId);
            return Ok(regions);
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateClient(int clientId, [FromBody] ClientRequest request)
        {
            var result = await _clientService.UpdateClientAsync(clientId, request);
            if (!result.IsSuccess)
            {
                return result.Error!.Contains("not found") ?
                                  NotFound(result.Error) :
                                  BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
