using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;

namespace ClientService.Core.Mappers
{
    public static class ClientMapper
    {
        public static CommunicationPreferencesDto ToClientCommunicationPreferencesDto(
        this DbClient client,
        DbClientAdresseComplement Adresse = null)
        {
            if (client == null)
            {
                return null;
            }

            var dto = new CommunicationPreferencesDto
            {
                // Map Client properties
                OkPourMailing = client.OkPourMailing != 0,
                OkPourMailingPartner = client.OkPourMailingPartner != 0,
                OkPourMailingAff = client.OkPourMailingAff != 0,
                OkPourSms = client.OkPourSms != 0,
                OkPourSmsPartner = client.OkPourSmsPartner != 0,
                OkPourSmsAff = client.OkPourSmsAff != 0
            };

            // If primary address is provided as an argument, use it
            if (Adresse != null)
            {
                dto.OkPourEnvoiPostal = Adresse.OkPourEnvoiPostal != 0;
                dto.OkPourEnvoiPostalPartner = Adresse.OkPourEnvoiPostalPartner != 0;
                dto.OkPourEnvoiPostalAff = Adresse.OkPourEnvoiPostalAff != 0;
            }
            return dto;
        }
    }
}
