using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{

    [Table("CLIENT_COMMANDE")]
    public class DbClientCommande
    {
        [Key]
        [Column("ID_COMMANDEC")]
        public long IdCommandec { get; set; }

        [Column("FDATE")]
        public DateTime? FDate { get; set; } = DateTime.Now;

        [Column("MONTANT")]
        public decimal? Montant { get; set; }

        [Column("ID_CLIENT")]
        public long IdClient { get; set; }

        [Column("ID_STRUCTURE")]
        public int? IdStructure { get; set; }

        [Column("ANNULATION")]
        public int? Annulation { get; set; } = 0;

        [Column("EXPORTEE")]
        public int? Exportee { get; set; } = 0;

        [Column("APURABLE")]
        public int? Apurable { get; set; } = 1;

        [Column("COMMENTAIRE")]
        public string? Commentaire { get; set; }

        [Column("NUM_COMMANDE")]
        public long? NumCommande { get; set; }

        [Column("ID_ETAT_COMMANDE")]
        public int? IdEtatCommande { get; set; } = 0;

        [Column("POIDS_GRAMME")]
        public int? PoidsGramme { get; set; }

        [Column("CONTROLE_FIANET")]
        public int? ControleFianet { get; set; }

        [Column("ID_EMPLOYE")]
        public int? IdEmploye { get; set; }

        [Column("EC_EN_ATTENTE")]
        public int? EcEnAttente { get; set; } = 0;

        [Column("FACT_NUM_CLIENT")]
        public long? FactNumClient { get; set; }

        [Column("FACT_PARTICULIER")]
        public int? FactParticulier { get; set; } = 1;

        [Column("FACT_TITRE")]
        public string? FactTitre { get; set; }

        [Column("FACT_NOM")]
        public string? FactNom { get; set; }

        [Column("FACT_PRENOM")]
        public string? FactPrenom { get; set; }

        [Column("FACT_ID_TYPE_CLIENT")]
        public int? FactIdTypeClient { get; set; } = 1;

        [Column("FACT_RAISON_SOCIALE")]
        public string? FactRaisonSociale { get; set; }

        [Column("FACT_ADRESSE1")]
        public string? FactAdresse1 { get; set; }

        [Column("FACT_ADRESSE2")]
        public string? FactAdresse2 { get; set; }

        [Column("FACT_TELEPHONE")]
        public string? FactTelephone { get; set; }

        [Column("FACT_PORTABLE")]
        public string? FactPortable { get; set; }

        [Column("FACT_PAYS")]
        public string? FactPays { get; set; }

        [Column("FACT_NUM_VOIE")]
        public int? FactNumVoie { get; set; }

        [Column("FACT_BTQC")]
        public string? FactBtqc { get; set; }

        [Column("FACT_TYPE_VOIE")]
        public string? FactTypeVoie { get; set; }

        [Column("FACT_CP")]
        public string? FactCp { get; set; }

        [Column("FACT_COMMUNE")]
        public string? FactCommune { get; set; }

        [Column("FACT_ID_CP")]
        public int? FactIdCp { get; set; } = 0;

        [Column("LIVR_ADRESSE1")]
        public string? LivrAdresse1 { get; set; }

        [Column("LIVR_ADRESSE2")]
        public string? LivrAdresse2 { get; set; }

        [Column("LIVR_TELEPHONE")]
        public string? LivrTelephone { get; set; }

        [Column("LIVR_PORTABLE")]
        public string? LivrPortable { get; set; }

        [Column("LIVR_PAYS")]
        public string? LivrPays { get; set; }

        [Column("LIVR_NUM_VOIE")]
        public int? LivrNumVoie { get; set; }

        [Column("LIVR_BTQC")]
        public string? LivrBtqc { get; set; }

        [Column("LIVR_TYPE_VOIE")]
        public string? LivrTypeVoie { get; set; }

        [Column("LIVR_CP")]
        public string? LivrCp { get; set; }

        [Column("LIVR_COMMUNE")]
        public string? LivrCommune { get; set; }

        [Column("LIVR_ID_CP")]
        public int? LivrIdCp { get; set; } = 0;

        [Column("FACT_MAIL")]
        public string? FactMail { get; set; }

        [Column("FACT_FAX")]
        public string? FactFax { get; set; }

        [Column("FACT_BATESC")]
        public string? FactBatesc { get; set; }

        [Column("NOTIFICATION_FIANET")]
        public string? NotificationFianet { get; set; }

        [Column("ID_FACTUREC")]
        public int? IdFacturec { get; set; }

        [Column("CODE")]
        public int? Code { get; set; } = 1;

        [Column("ID_DEVISE")]
        public int? IdDevise { get; set; } = 0;

        [Column("TAUX_DEVISE")]
        public double? TauxDevise { get; set; } = 1;

        [Column("FDATE_INSERTION")]
        public DateTime? FDateInsertion { get; set; } = DateTime.Now;

        [Column("TOTAL_DEVISE")]
        public decimal? TotalDevise { get; set; }

        [Column("LIVR_DIGICODE")]
        public string? LivrDigicode { get; set; }

        [Column("FACT_DIGICODE")]
        public string? FactDigicode { get; set; }

        [Column("SCORING_EN_COURS")]
        public int? ScoringEnCours { get; set; }

        [Column("BASE_DEST_LOGISTIQUE_WES")]
        public string? BaseDestLogistiqueWes { get; set; }
    }
}
