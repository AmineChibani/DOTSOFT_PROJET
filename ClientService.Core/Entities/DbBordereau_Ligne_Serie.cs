using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClientService.Core.Entities
{
    [Table("BORDEREAU_LIGNE_SERIE", Schema = "DOTSOFT")]
    public class DbBordereau_Ligne_Serie
    {
        [Key]
        [Column("ID_SERIE")]
        public int Id_Serie { get; set; }

        [Column("ID_PRODUIT")]
        public int? Id_Produit { get; set; }

        [Column("ID_BORDEREAU")]
        public int? Id_Borederau { get; set; }

        [Column("ID_LIGNE")]
        public int? Id_Ligne { get; set; }

        [Column("SERIE")]
        public string? Serie { get; set; }

        [Column("DATELIMITE")]
        public DateTime? DateLimite { get; set; }
    }
}
