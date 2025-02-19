using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{

    [Table("ECOMMERCE_BA", Schema = "DOTSOFT")]
    public class DbEcommerceBa
    {
        [Column("ID_BA")]
        [Key]
        public int IdBa { get; set; }

        [Column("ANNULE")]
        public int? Annule { get; set; }

        [Column("DATE_ANNULATION")]
        public DateTime? DateAnnulation { get; set; }
    }
}
