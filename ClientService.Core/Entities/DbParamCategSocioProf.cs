using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_CATEG_SOCIO_PROF", Schema = "DOTSOFT")]
    public class DbParamCategSocioProf
    {
        [Column("ID_CSP")]
        [Key]
        public int IdCsp { get; set; }

        [Column("NOM")]
        public string Nom { get; set; }

        [Column("CODE_EXTERNE")]
        public string CodeExterne { get; set; }

        public virtual DbLanguageParamCategSocioProf LanguageParamCategSocioPro { get; set; }
    }
}
