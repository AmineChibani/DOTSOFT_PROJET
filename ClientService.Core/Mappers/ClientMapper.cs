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
        public static GetOptinBaseDto ToClientOptinDto(
            this DbClient client, DbClientAdresseComplement adresse, bool affiliate)
        {

            var baseDto = new GetOptinBaseDto
            {
                OkPourMailing = client.OkPourMailing != 0,
                OkPourMailingPartner = client.OkPourMailingPartner != 0,
                OkPourSms = client.OkPourSms != 0,
                OkPourSmsPartner = client.OkPourSmsPartner != 0,
                OkPourEnvoiPostal = adresse.OkPourEnvoiPostal != 0,
                OkPourEnvoiPostalPartner = adresse.OkPourEnvoiPostalPartner != 0
            };

            if (!affiliate) return baseDto;

            // Use explicit conversion to FullDto
            return new GetOptinFullDto(baseDto)
            {
                OkPourMailingAff = client.OkPourMailingAff != 0,
                OkPourSmsAff = client.OkPourSmsAff != 0,
                OkPourEnvoiPostalAff = adresse.OkPourEnvoiPostalAff != 0
            };
        }
    }

}
