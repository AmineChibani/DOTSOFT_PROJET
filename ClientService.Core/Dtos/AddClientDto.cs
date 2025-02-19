using ClientService.Core.Entities;

namespace ClientService.Infrastructure.Dtos
{
    public class AddClientDto
    {
        public string Nom { get; set; } = string.Empty;

        public string? Nom2 { get; set; }

        public string Prenom { get; set; } = string.Empty;

        public int? TitreId { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public string Mail { get; set; } = string.Empty;

        public DateTime? Fdate { get; set; }
        public int? PointsNumber { get; set; }

        public int? IdTypeClient { get; set; }

        public int? Particulier { get; set; }

        public int? StructureId { get; set; }

        public int? PourMailing { get; set; }

        public int? Mailing { get; set; }

        public int? OkPourMailing { get; set; }

        public int? PremierAchatPlus { get; set; }

        public int? EmployeId { get; set; }

        public int? EmployeIdModification { get; set; }

        public DateTime? FDateModification { get; set; }

        public string? Commentaire { get; set; }

        public string? Interets { get; set; }

        public string? CodeExterne { get; set; }

        public int? NumClient { get; set; }

        public int? OkPourSms { get; set; }

        public int? OkPourSmsAff { get; set; }

        public int? OkPourSmsPartner { get; set; }

        public int? OkPourMailingAff { get; set; }

        public int? OkPourMailingPartner { get; set; }

        public int? Eticket { get; set; }

        public int? WebType { get; set; }
        public string? LieuNaissance { get; set; }

        public string? CarteFidelite1 { get; set; }

        public string? RaisonSociale { get; set; }

        public string? LivrRaisonSociale { get; set; }


        public List<DbClientAdresse> ClientAdresses { get; set; }

        public List<DbClientAdresseComplement> ClientAdresseComplement { get; set; }

        public DbClientOptin ClientOptin { get; set; }
    }
}
