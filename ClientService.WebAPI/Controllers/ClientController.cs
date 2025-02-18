﻿using ClientService.Core.Entities;
using ClientService.Core.Interfaces;
using ClientService.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
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
                LivrRaisonSociale = newClientDto.LivrRaisonSociale,
                ClientAdresses = newClientDto.ClientAdresses ?? new List<DbClientAdresse>(),
                ClientAdresseComplement = newClientDto.ClientAdresseComplement ?? new List<DbClientAdresseComplement>(),
                ClientOptin = newClientDto.ClientOptin ,
              };

            var Client = await _clientService.AddClient(newClient);
            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Nom }, newClient);
        }
    }
}
