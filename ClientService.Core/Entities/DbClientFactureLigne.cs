using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{

    [Table("CLIENT_FACTURE_LIGNE", Schema = "DOTSOFT")]
    public class DbClientFactureLigne
    {
        [Column("ID_LIGNE")]
        [Key]
        public int IdLigne { get; set; }

        [Column("ID_FACTUREC")]
        public int IdFactureC { get; set; }

        [Column("ID_PRODUIT")]
        public int IdProduit { get; set; }

        [Column("ID_TAXE")]
        public int IdTaxe { get; set; }

        [Column("MONTANT")]
        public Decimal? Montant { get; set; }

        [Column("MONTANT_ACHAT")]
        public Decimal? MontantAchat { get; set; }

        [Column("QUANTITE")]
        public int Quantite { get; set; }

        [Column("MONTANT_TVA")]
        public Decimal? MontantTva { get; set; }

        public virtual DbClientFacture ClientFacture { get; set; }
    }
}
