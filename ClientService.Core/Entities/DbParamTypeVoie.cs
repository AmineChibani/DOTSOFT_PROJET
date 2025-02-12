using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_TYPE_DE_VOIE")]
    public class DbParamTypeVoie
    {
        [Column("ID_TYPE_VOIE")]
        [Key]
        public int IdTypeVoie { get; set; }

        [Column("CODE")]
        public string Code { get; set; }

        [Column("LIBELLE")]
        public string Libelle { get; set; }

        [Column("DEFAUT")]
        public int? Defaut { get; set; }
    }
}
