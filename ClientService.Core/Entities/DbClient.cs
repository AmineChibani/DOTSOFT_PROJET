using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{
    [Table("CLIENT", Schema = "DOTSOFT")]
    public class DbClient
    {
        [Column("ID_CLIENT")]
        [Key]
        public int ClientId { get; set; }

        [Column("ID_STRUCTURE_ORIGINE")]
        public int? StructureOriginId { get; set; }

        [Column("NOM")]
        public string? Nom { get; set; }

        [Column("NOM2")]
        public string? Nom2 { get; set; }

        [Column("PRENOM")]
        public string? Prenom { get; set; }

        [Column("NOMPRENOM")]
        public string? FirstLastName { get; set; }

        [Column("ID_TITRE")]
        public int? TitreId { get; set; }

        [Column("EN_LOCAL")]
        public int? Local { get; set; }

        [Column("DATE_NAISSANCE")]
        public DateTime? BirthdayDate { get; set; }

        [Column("FDATE")]
        public DateTime? Fdate { get; set; }

        [Column("MAIL")]
        public string? Mail { get; set; }

        [Column("NBRE_POINTS")]
        public int? PointsNumber { get; set; }

        [Column("ID_TYPE_CLIENT")]
        public int? IdTypeClient { get; set; }

        [Column("PARTICULIER")]
        public int? Particulier { get; set; }

        [Column("ID_STRUCTURE")]
        public int? StructureId { get; set; }

        [Column("POURMAILING")]
        public int? PourMailing { get; set; }

        [Column("MAILING")]
        public int? Mailing { get; set; }

        [Column("OK_POUR_MAILING")]
        public int? OkPourMailing { get; set; }

        [Column("PREMIER_ACHAT_PLUS")]
        public int? PremierAchatPlus { get; set; }

        [Column("ID_EMPLOYE")]
        public int? EmployeId { get; set; }

        [Column("ID_EMPLOYE_MODIFICATION")]
        public int? EmployeIdModification { get; set; }

        [Column("FDATEMODIFICATION")]
        public DateTime? FDateModification { get; set; }

        [Column("COMMENTAIRE")]
        public string? Commentaire { get; set; }

        [Column("INTERETS")]
        public string? Interets { get; set; }

        [Column("CODE_EXTERNE")]
        public string? CodeExterne { get; set; }

        [Column("NUM_CLIENT")]
        public int? NumClient { get; set; }

        [Column("OK_POUR_SMS")]
        public int? OkPourSms { get; set; }

        [Column("OK_POUR_SMS_AFF")]
        public int? OkPourSmsAff { get; set; }

        [Column("OK_POUR_SMS_PARTNER")]
        public int? OkPourSmsPartner { get; set; }

        [Column("OK_POUR_MAILING_AFF")]
        public int? OkPourMailingAff { get; set; }

        [Column("OK_POUR_MAILING_PARTNER")]
        public int? OkPourMailingPartner { get; set; }

        [Column("ETICKET")]
        public int? Eticket { get; set; }

        [Column("WEBTYPE")]
        public int? WebType { get; set; }

        [Column("LIEU_NAISSANCE")]
        public string? LieuNaissance { get; set; }

        [Column("CARTE_FIDELITE1")]
        public string? CarteFidelite1 { get; set; }

        [Column("RAISON_SOCIALE")]
        public string? RaisonSociale { get; set; }

        [Column("LIVR_RAISON_SOCIALE")]
        public string? LivrRaisonSociale { get; set; }

        public List<DbClientAdresse> ClientAdresses { get; set; } = new List<DbClientAdresse>();

        public List<DbClientAdresseComplement> ClientAdresseComplement { get; set; } = new List<DbClientAdresseComplement>();

        public virtual DbClientOptin? ClientOptin { get; set; }
        [ForeignKey("IdTypeClient")]
        public virtual DbClientType ClientType { get; set; }

    }   
}
