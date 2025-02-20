using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("SAV_DOSSIER")]
    public class DbSavDossier
    {
        [Column("ID_DOSSIER")]
        [Key]
        public long ID_DOSSIER { get; set; }

        [Column("ID_CLIENT")]
        public long ID_CLIENT { get; set; }

        [Column("ID_PRODUIT")]
        public long? ID_PRODUIT { get; set; }

        [Column("ID_FACTUREC")]
        public long? ID_FACTUREC { get; set; }

        [Column("OUVERT")]
        public int OUVERT { get; set; } = 1;

        [Column("ID_ETAT")]
        public int ID_ETAT { get; set; } = 1;

        [Column("CLIENT_A_PREVENIR")]
        public int CLIENT_A_PREVENIR { get; set; } = 0;

        [Column("CLIENT_PREVENU")]
        public int CLIENT_PREVENU { get; set; } = 0;

        [Column("FDATE")]
        public DateTime FDATE { get; set; } = DateTime.Now;

        [Column("APPAREIL_EN_STOCK")]
        public int APPAREIL_EN_STOCK { get; set; } = 0;

        [Column("ID_EMPLOYE")]
        public long ID_EMPLOYE { get; set; }

        [Column("ID_STRUCTURE")]
        public long ID_STRUCTURE { get; set; }

        [Column("ID_STRUCTURE_GESTION")]
        public long ID_STRUCTURE_GESTION { get; set; }

        [Column("ID_STATION_TECHNIQUE")]
        public long ID_STATION_TECHNIQUE { get; set; } = 0;

        [Column("DATE_TRANSFERT_SAV")]
        public DateTime? DATE_TRANSFERT_SAV { get; set; }

        [Column("DATE_TRANSFERT_STATION")]
        public DateTime? DATE_TRANSFERT_STATION { get; set; }

        [Column("ID_FAMILLE")]
        public long ID_FAMILLE { get; set; }

        [Column("ID_COULEUR")]
        public long ID_COULEUR { get; set; }

        [Column("REFERENCE")]
        public string REFERENCE { get; set; }

        [Column("SECTEUR")]
        public string SECTEUR { get; set; }

        [Column("RAYON")]
        public string RAYON { get; set; }

        [Column("FAMILLE")]
        public string FAMILLE { get; set; }

        [Column("SOUS_FAMILLE")]
        public string SOUS_FAMILLE { get; set; }

        [Column("ID_MARQUE")]
        public long ID_MARQUE { get; set; }

        [Column("MARQUE")]
        public string MARQUE { get; set; }

        [Column("NUM_SERIE")]
        public string NUM_SERIE { get; set; }

        [Column("REF_TECHNIQUE")]
        public string REF_TECHNIQUE { get; set; }

        [Column("DATE_ACHAT")]
        public DateTime? DATE_ACHAT { get; set; }

        [Column("VALEUR_ACHAT")]
        public decimal VALEUR_ACHAT { get; set; }

        [Column("POURCENT")]
        public int POURCENT { get; set; } = 100;

        [Column("PRIX_ACHAT")]
        public decimal PRIX_ACHAT { get; set; }

        [Column("ID_STRUCTURE_ACHAT")]
        public long ID_STRUCTURE_ACHAT { get; set; }

        [Column("STRUCTURE_AUTRE")]
        public string STRUCTURE_AUTRE { get; set; }

        [Column("GARANTIE_DUREE")]
        public int GARANTIE_DUREE { get; set; } = 1;

        [Column("GARANTIE_JUSTIFICATIF")]
        public int GARANTIE_JUSTIFICATIF { get; set; } = 0;

        [Column("DESCRIPTIF_PANNE")]
        public string DESCRIPTIF_PANNE { get; set; }

        [Column("DEVIS")]
        public int DEVIS { get; set; } = 0;

        [Column("MONTANT_MAX")]
        public decimal MONTANT_MAX { get; set; }

        [Column("ID_PRET")]
        public long ID_PRET { get; set; }

        [Column("ID_PRODUIT_PRET")]
        public long ID_PRODUIT_PRET { get; set; }

        [Column("CAUTION")]
        public decimal CAUTION { get; set; }

        [Column("CAUTION_RENDUE")]
        public int CAUTION_RENDUE { get; set; } = 0;

        [Column("LOCALISATION")]
        public string LOCALISATION { get; set; }

        [Column("ECHANGE")]
        public int ECHANGE { get; set; } = 0;

        [Column("ID_PLANNING")]
        public long ID_PLANNING { get; set; } = 0;

        [Column("ID_FACTURE_GENERATION_ECHANGE")]
        public long? ID_FACTURE_GENERATION_ECHANGE { get; set; }

        [Column("NUM_FACTURE_GENERATION_ECHANGE")]
        public long? NUM_FACTURE_GENERATION_ECHANGE { get; set; }

        [Column("ID_FACTURE_ECHANGE")]
        public long? ID_FACTURE_ECHANGE { get; set; }

        [Column("NUM_FACTURE_ECHANGE")]
        public long? NUM_FACTURE_ECHANGE { get; set; }

        [Column("ID_PRODUIT_ECHANGE")]
        public long? ID_PRODUIT_ECHANGE { get; set; }

        [Column("ECHANGE_DATE_VALIDITE")]
        public DateTime? ECHANGE_DATE_VALIDITE { get; set; }

        [Column("ID_BORDEREAU")]
        public long ID_BORDEREAU { get; set; }

        [Column("CLIENT_TITRE")]
        public string CLIENT_TITRE { get; set; }

        [Column("CLIENT_NOM")]
        public string CLIENT_NOM { get; set; }

        [Column("CLIENT_PRENOM")]
        public string CLIENT_PRENOM { get; set; }

        [Column("ID_PRODUIT_RETOUR")]
        public long? ID_PRODUIT_RETOUR { get; set; }

        [Column("ID_FACTURE_RETRO")]
        public long? ID_FACTURE_RETRO { get; set; }

        [Column("ID_TYPE")]
        public long ID_TYPE { get; set; }

        [Column("RMA")]
        public long RMA { get; set; }

        [Column("NB_CFR")]
        public int NB_CFR { get; set; }

        [Column("QUALITE_EMBAL")]
        public int QUALITE_EMBAL { get; set; }

        [Column("ETAT_MARCHE")]
        public int ETAT_MARCHE { get; set; } = 0;

        [Column("NB_MANQUANT")]
        public int NB_MANQUANT { get; set; }

        [Column("POURCENT_DPA")]
        public int POURCENT_DPA { get; set; }

        [Column("ID_COMMANDEC")]
        public long ID_COMMANDEC { get; set; }

        [Column("ID_ETAT_PRODUIT")]
        public long ID_ETAT_PRODUIT { get; set; }

        [Column("POURCENTAGE_ETAT_PRODUIT")]
        public int POURCENTAGE_ETAT_PRODUIT { get; set; }

        [Column("CONFORMITE_PRODUIT")]
        public int CONFORMITE_PRODUIT { get; set; }

        [Column("COMMENTAIRE_ETAT_PRODUIT")]
        public string COMMENTAIRE_ETAT_PRODUIT { get; set; }

        [Column("CONTREPARTIE")]
        public string CONTREPARTIE { get; set; }

        [Column("COMMENTAIRE_CLIENT")]
        public string COMMENTAIRE_CLIENT { get; set; }

        [Column("ID_PANNE")]
        public long ID_PANNE { get; set; }
    }
}
