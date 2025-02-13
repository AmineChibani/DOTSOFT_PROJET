using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ClientService.Core.Entities
{
    [Table("CLIENT_FACTURE")]
    public class DbClientFacture
    {
        [Column("ID_FACTUREC")]
        [Key]
        public int IdFactureC { get; set; }

        [Column("ID_COMMANDEC")]
        public int IdCommandeC { get; set; }

        [Column("ID_CLIENT")]
        public int IdClient { get; set; }

        [Column("NUM_FACTURE")]
        public int? NumFacture { get; set; }

        [Column("NB_POINTS_UTILISES")]
        public int NbPointsUtilises { get; set; }

        [Column("FDATE")]
        public DateTime Fdate { get; set; }

        [Column("MONTANT")]
        public Decimal? Montant { get; set; }

        [Column("MONTANT_TVA")]
        public double? MontantTva { get; set; }

        [Column("TYPE_FACTURE")]
        public string TypeFacture { get; set; }

        [Column("ANNULATION")]
        public int? Annulation { get; set; }

        [Column("AVOIR")]
        public int? Avoir { get; set; }

        [Column("ID_STRUCTURE")]
        public int? IdStructure { get; set; }

        public List<DbClientFactureLigne> ClientFactureLignes { get; set; }
    }
}
