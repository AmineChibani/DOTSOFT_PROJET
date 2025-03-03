using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{

    [Table("CLIENT_OPERATION", Schema = "DOTSOFT")]
    public class DbClientOperation
    {
        [Column("ID_OPERATION")]
        [Key]
        public int IdOperation { get; set; }

        [Column("COMPTE_CLIENT")]
        public Decimal? CompteClient { get; set; }

        [Column("ID_TYPE_REGLEMENT")]
        public int? IdTypeReglement { get; set; }

        [Column("ID_CLIENT")]
        public int IdClient { get; set; }

        [Column("ID_STRUCTURE")]
        public int? IdStructure { get; set; }

        [Column("FDATE")]
        public DateTime? Fdate { get; set; }

        [Column("TYPE_DOCUMENT")]
        public string TypeDocument { get; set; }

        
        public virtual DbFactureTypeReglement FactureTypeReglement { get; set; }

        public DbClient Client { get; set; }

    }
}
