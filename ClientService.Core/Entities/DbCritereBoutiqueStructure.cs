using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("CRITEREBOUTIQUE_STRUCTURE", Schema = "COM02")]
    public class CritereBoutiqueStructure
    {
        [Key]
        [Column("ID_CRITERE")]
        public long IdCritere { get; set; }

        [Key]
        [Column("ID_STRUCTURE")]
        public long IdStructure { get; set; }

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
    }
}
