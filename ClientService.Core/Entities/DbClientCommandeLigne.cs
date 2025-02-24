using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{

    [Table("CLIENT_COMMANDE_LIGNE")]
    public class DbClientCommandeLigne
    {
        [Column("ID_LIGNE")]
        [Key]
        public long? IdLigne { get; set; }

        [Column("ID_COMMANDEC")]
        public long? IdCommandec { get; set; }

        [Column("ID_PRODUIT")]
        public long? IdProduit { get; set; }

        [Column("ID_TAXE")]
        public int? IdTaxe { get; set; }

        [Column("QUANTITE")]
        public int? Quantite { get; set; }

        [Column("MONTANT")]
        public double? Montant { get; set; }

        [Column("MONTANT_REMISE")]
        public double? MontantRemise { get; set; }

        [Column("POURCENTAGE_REMISE")]
        public double? PourcentageRemise { get; set; }

        [Column("QUANTITE_LIVREE")]
        public int? QuantiteLivree { get; set; }

        [Column("ID_MODE_ENLEVEMENT")]
        public int? IdModeEnlevement { get; set; }

        [Column("MONTANT_TVA")]
        public double? MontantTva { get; set; }

        [Column("QUANTITE_FACTUREE")]
        public int? QuantiteFacturee { get; set; }

        [Column("ETAT")]
        public int? Etat { get; set; }

        [Column("ETATDATE")]
        public DateTime? EtatDate { get; set; }

        [Column("ETATHISTORIQUE")]
        public string? EtatHistorique { get; set; }

        [Column("ID_BORDEREAU")]
        public long? IdBordereau { get; set; }

        [Column("ETATVISIBLE")]
        public bool? EtatVisible { get; set; }

        [Column("ID_EMPLOYE")]
        public int? IdEmploye { get; set; }

        [Column("COMMENTAIRE")]
        public string? Commentaire { get; set; }

        [Column("NOUVEAU_LIBELLE")]
        public string? NouveauLibelle { get; set; }

        [Column("NOMENCLATURE")]
        public string? Nomenclature { get; set; }

        [Column("SERIE")]
        public string? Serie { get; set; }

        [Column("PMD")]
        public string? Pmd { get; set; }

        [Column("EC_QUANTITE_DEJA_PREPAREE")]
        public int? EcQuantiteDejaPreparee { get; set; }

        [Column("EC_QUANTITE_FACTUREE")]
        public int? EcQuantiteFacturee { get; set; }

        [Column("EC_QUANTITE_RESERVEE")]
        public int? EcQuantiteReservee { get; set; }

        [Column("ID_PRODUIT_ATTACHEMENT")]
        public long? IdProduitAttachement { get; set; }

        [Column("ID_LIGNE_PRODUIT_ATTACHEMENT")]
        public long? IdLigneProduitAttachement { get; set; }

        [Column("EC_QUANTITE_SUPPRIMEE")]
        public int? EcQuantiteSupprimee { get; set; }

        [Column("PRIX_ACHAT_UNITAIRE")]
        public decimal? PrixAchatUnitaire { get; set; }

        [Column("EC_QUANTITE_POUR_RAPPEL")]
        public int? EcQuantitePourRappel { get; set; }

        [Column("EC_QUANTITE_SUPPRIMEE_POUR_RAP")]
        public int? EcQuantiteSupprimeePourRap { get; set; }

        [Column("ID_PACK")]
        public string? IdPack { get; set; }

        [Column("PRIX_PACK")]
        public double? PrixPack { get; set; }

        [Column("NOM_PACK")]
        public string? NomPack { get; set; }

        [Column("EN_STOCK")]
        public bool? EnStock { get; set; }

        [Column("QTE_PROD_VALEUR")]
        public int? QteProdValeur { get; set; }

        [Column("ID_TRANSPORTEUR_BO")]
        public int? IdTransporteurBo { get; set; }

        [Column("DEMANDE_SUIVI")]
        public bool? DemandeSuivi { get; set; }

        [Column("EC_QUANTITE_RETOURNEE")]
        public int? EcQuantiteRetournee { get; set; }

        [Column("CODE_POINT_RELAIS")]
        public string? CodePointRelais { get; set; }

        [Column("ADRESSE1_POINT_RELAIS")]
        public string? Adresse1PointRelais { get; set; }

        [Column("ADRESSE2_POINT_RELAIS")]
        public string? Adresse2PointRelais { get; set; }

        [Column("CP_POINT_RELAIS")]
        public string? CpPointRelais { get; set; }

        [Column("VILLE_POINT_RELAIS")]
        public string? VillePointRelais { get; set; }

        [Column("DATE_PREVUE_EXPEDITION")]
        public DateTime? DatePrevueExpedition { get; set; }

        [Column("ID_ETAT")]
        public char? IdEtat { get; set; }

        [Column("FLAG_DK_BE")]
        public int? FlagDkBe { get; set; }

        [Column("COMPTEUR_VENTE")]
        public int? CompteurVente { get; set; }

        [Column("ID_TYPE_COMMANDE")]
        public int? IdTypeCommande { get; set; }

        [Column("MONTANT_BASE")]
        public decimal? MontantBase { get; set; }

        [Column("ID_ETAT_COMMANDE_LIGNE")]
        public int? IdEtatCommandeLigne { get; set; }

        [Column("QUANTITE_ANNULEE")]
        public int? QuantiteAnnulee { get; set; }

        [Column("GENERATION_BA")]
        public bool GenerationBa { get; set; }
    }
}
