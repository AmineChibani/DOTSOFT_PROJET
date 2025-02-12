using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("FACTURE_TYPE_REGLEMENT")]
    public class DbFactureTypeReglement
    {
        [Column("ID_TYPE_REGLEMENT")]
        [Key]
        public int IdTypeReglement { get; set; }

        [Column("COMPTANT")]
        public int? Comptant { get; set; }

        public ICollection<DbClientOperation> ClientOperations { get; set; }
    }
}
