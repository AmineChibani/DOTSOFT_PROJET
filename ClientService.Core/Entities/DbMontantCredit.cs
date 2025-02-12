using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("CREDIT_CLIENT_HIER")]
    public class DbMontantCredit
    {
        [Column("ID_STRUCTURE")]
        [Key]
        public int IdStructure { get; set; }

        [Column("ID_CLIENT")]
        public int IdClient { get; set; }

        [Column("MONTANT_CREDIT")]
        public Decimal? MontantCredit { get; set; }
    }
}
