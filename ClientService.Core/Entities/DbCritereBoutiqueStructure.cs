using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("CRITEREBOUTIQUE_STRUCTURE")]
    public class DbCritereBoutiqueStructure
    {
        [Key]
        [Column("ID_CRITERE")]
        public int IdCritere { get; set; }

        [Column("ID_STRUCTURE")]
        public int IdStructure { get; set; }

        [Column("VALEUR")]
        [MaxLength(255)]
        public string Valeur { get; set; }

        [Column("ID_EMPLOYE_INSERTION")]
        public int? IdEmployeInsertion { get; set; }

        [Column("FDATE_INSERTION")]
        public DateTime? FdateInsertion { get; set; }

        [Column("ID_EMPLOYE_MODIFICATION")]
        public int? IdEmployeModification { get; set; }

        [Column("FDATE_MODIFICATION")]
        public DateTime? FdateModification { get; set; }

        public DbStructure structure { get; set; }
    }
}
