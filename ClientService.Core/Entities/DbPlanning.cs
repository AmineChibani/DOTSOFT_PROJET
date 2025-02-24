using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PLANNING")]
    public class DbPlanning
    {
        [Column("ID_PLANNING")]
        [Key]

        public long? IdPlanning { get; set; }

        [Column("ID_STRUCTURE")]
        public int? IdStructure { get; set; }

        [Column("ID_VOCATION")]
        public int? IdVocation { get; set; }

        [Column("ID_EQUIPE")]
        public int? IdEquipe { get; set; }

        [Column("ID_TRANCHE_DEB")]
        public int? IdTrancheDeb { get; set; }

        [Column("ID_TRANCHE_FIN")]
        public int? IdTrancheFin { get; set; }

        [Column("JOUR")]
        public int? Jour { get; set; }

        [Column("DATEJOUR")]
        public int? DateJour { get; set; }

        [Column("DATEMOIS")]
        public int? DateMois { get; set; }

        [Column("DATEANNEE")]
        public int? DateAnnee { get; set; }

        [Column("FDATE")]
        public DateTime? FDate { get; set; }

        [Column("ID_FACTUREC")]
        public long? IdFacturec { get; set; }

        [Column("ID_DOSSIER")]
        public long? IdDossier { get; set; }

        [Column("LIVRAISON_VALIDEE")]
        public bool? LivraisonValidee { get; set; }

        [Column("TITRE")]
        public string? Titre { get; set; }

        [Column("TEXTE")]
        public string? Texte { get; set; }

        [Column("DATECREATION")]
        public DateTime? DateCreation { get; set; }

        [Column("DATEMODIFICATION")]
        public DateTime? DateModification { get; set; }

        [Column("ID_EMPLOYE")]
        public int? IdEmploye { get; set; }

        [Column("ID_EMPLOYE_AFFECT")]
        public int? IdEmployeAffect { get; set; }

        [Column("NOM_CLIENT")]
        public string? NomClient { get; set; }

        [Column("PRENOM_CLIENT")]
        public string? PrenomClient { get; set; }

        [Column("TEL_CLIENT")]
        public string? TelClient { get; set; }
    }
}
