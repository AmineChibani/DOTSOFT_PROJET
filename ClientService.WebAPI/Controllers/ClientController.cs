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


        [HttpGet("GetAddressByClientId/{clientId}")]
        public async Task<IActionResult> GetAddressByClientId(int clientId)
        {
            var result = await _clientService.GetAddressByClientId(clientId);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
