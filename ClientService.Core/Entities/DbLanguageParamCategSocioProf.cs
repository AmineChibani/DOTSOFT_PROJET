using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("LANGUAGE_PARAM_CATEG_SOCIO_PRO")]
    public class DbLanguageParamCategSocioProf
    {
        [Column("ID_CSP")]
        [Key]
        public int IdCsp { get; set; }

        [Column("CODE")]
        public int Code { get; set; }

        [Column("NOM")]
        public string Nom { get; set; }

        public ICollection<DbParamCategSocioProf> DbParamCategSocioProfs { get; set; }
    }
}
