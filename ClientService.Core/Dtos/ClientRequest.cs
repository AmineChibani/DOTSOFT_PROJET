using ClientService.Core.Entities;

namespace ClientService.Infrastructure.Dtos
{
    public class ClientRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LastName2 { get; set; }
        public int? EmployeId { get; set; }
        public string? RaisonSociale { get; set; }

        public int StructureId { get; set; }
        public int? TypeVoie { get; set; }

        public bool OkPourSms { get; set; }
        public bool OkPourMailing { get; set; }
        public bool OkPourMailingAff { get; set; }
        public bool OkPourSmsPartner { get; set; }
        public bool OkPourSmsAff { get; set; }
        public bool Eticket { get; set; }
        public bool Particulier { get; set; }

        public List<ClientAdresse> ClientAdressesRequest { get; set; }
        public List<ClientAdresseComplement> ClientAdresseComplementRequest { get; set; }
        public ClientOptinRequest ClientOptinRequest { get; set; }
    }

    public class ClientAdresse
    {
        public int AdresseTypeId { get; set; }
        public string? Adresse1 { get; set; }
        public int? CpId { get; set; }
        public int? PaysId { get; set; }
        public string? CpEtranger { get; set; }
        public string? VilleEtranger { get; set; }
        public string? Btqc { get; set; }
        public string? Batesc { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CellPhone { get; set; }
        public string? Fax { get; set; }
    }

    public class ClientAdresseComplement
    {
        public int AdresseTypeId { get; set; }
        public bool OkPourEnvoiPostal { get; set; }
        public bool OkPourEnvoiPostalAff { get; set; }
        public bool OkPourEnvoiPostalPartner { get; set; }
    }

    public class ClientOptinRequest
    {
        public DateTime? DateOptinEmail { get; set; }
        public DateTime? DateOptinPostal { get; set; }
        public DateTime? DateOptinSms { get; set; }
        public DateTime? DateAffOptinEmail { get; set; }
        public DateTime? DateAffOptinPostal { get; set; }
        public DateTime? DateAffOptinSms { get; set; }
        public DateTime? DatePartnerOptinEmail { get; set; }
        public DateTime? DatePartnerOptinPostal { get; set; }
        public DateTime? DatePartnerOptinSms { get; set; }
    }
}
