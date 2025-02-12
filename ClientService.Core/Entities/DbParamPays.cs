using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_PAYS")]
    public class DbParamPays
    {
        [Column("ID_PAYS")]
        [Key]
        public int PaysId { get; set; }

        [Column("NOM")]
        public string Libelle { get; set; }
    }
}
