using ClientService.Core.Entities;

namespace ClientService.Core.Dtos
{
    public class Client
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LastName2 { get; set; }

        public string Email { get; set; }

        public int ClientId { get; set; }

        public string Phone { get; set; }

        public string Postal { get; set; }

        public string LoyaltyCard { get; set; }

        public int? StructureId { get; set; }

        public int? EmployeId { get; set; }

        public int? Mailing { get; set; }

        public int? OkPourMailing { get; set; }

        public int? PourMailing { get; set; }

        public int? NumClient { get; set; }

        public int? IdTypeClient { get; set; }

        public int? Particulier { get; set; }

        public int? TitreId { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public int? OkPourSms { get; set; }

        public int? OkPourSmsAff { get; set; }

        public int? OkPourSmsPartner { get; set; }

        public int? OkPourMailingAff { get; set; }

        public int? OkPourMailingPartner { get; set; }

        public int? Eticket { get; set; }

        public int? WebType { get; set; }

        public List<DbClientAdresse> ClientAdresses { get; set; }

        public List<DbClientAdresseComplement> ClientAdresseComplement { get; set; }

        public DbClientOptin ClientOptin { get; set; }
    }
}
