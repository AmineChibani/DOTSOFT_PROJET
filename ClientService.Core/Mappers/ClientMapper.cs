using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;
using ClientService.Infrastructure.Dtos;

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

        public static ClientRequest ToClientRequest(this DbClient client)
        {
            if (client == null)
            {
                return null;
            }

            return new ClientRequest
            {
                FirstName = client.Prenom,
                LastName = client.Nom,
                LastName2 = client.Nom2,
                EmployeId = client.EmployeId,
                RaisonSociale = client.RaisonSociale,
                StructureId = client.StructureId == null ? 0 : (int)client.StructureId,

                OkPourSms = client.OkPourSms == 1 ? true : false,
                OkPourMailing = client.OkPourMailing == 1 ? true : false,
                OkPourMailingAff = client.OkPourMailingAff == 1 ? true : false,
                OkPourSmsPartner = client.OkPourSmsPartner == 1 ? true : false,
                OkPourSmsAff = client.OkPourSmsAff == 1 ? true : false,
                Eticket = client.Eticket == 1 ? true : false,
                Particulier = client.Particulier == 1 ? true : false,

                ClientAdressesRequest = client.ClientAdresses?.Select(address => new ClientAdresse
                {
                    AdresseTypeId = address.AdresseTypeId,
                    Adresse1 = address.Adresse1,
                    CpId = address.CpId,
                    PaysId = address.PaysId,
                    CpEtranger = address.CpEtranger,
                    VilleEtranger = address.VilleEtranger,
                    Btqc = address.Btqc,
                    Batesc = address.Batesc,
                    PhoneNumber = address.PhoneNumber,
                    CellPhone = address.CellPhone,
                    Fax = address.Fax
                }).ToList() ?? new List<ClientAdresse>(),

                ClientAdresseComplementRequest = client.ClientAdresseComplement?.Select(complement => new ClientAdresseComplement
                {
                    AdresseTypeId = complement.AdresseTypeId,
                    OkPourEnvoiPostal = complement.OkPourEnvoiPostal == 1 ? true : false,
                    OkPourEnvoiPostalAff = complement.OkPourEnvoiPostalAff == 1 ? true : false,
                    OkPourEnvoiPostalPartner = complement.OkPourEnvoiPostalPartner == 1 ? true : false
                }).ToList() ?? new List<ClientAdresseComplement>(),

                ClientOptinRequest = client.ClientOptin != null ? new ClientOptinRequest
                {
                    DateOptinEmail = client.ClientOptin.DateOptinEmail,
                    DateOptinPostal = client.ClientOptin.DateOptinPostal,
                    DateOptinSms = client.ClientOptin.DateOptinSms,
                    DateAffOptinEmail = client.ClientOptin.DateAffOptinEmail,
                    DateAffOptinPostal = client.ClientOptin.DateAffOptinPostal,
                    DateAffOptinSms = client.ClientOptin.DateAffOptinSms,
                    DatePartnerOptinEmail = client.ClientOptin.DatePartnerOptinEmail,
                    DatePartnerOptinPostal = client.ClientOptin.DatePartnerOptinPostal,
                    DatePartnerOptinSms = client.ClientOptin.DatePartnerOptinSms
                } : null
            };
        }
    }

}
