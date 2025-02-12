using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_CODE_POSTAL")]
    public class DbParamCodePostal
    {
        [Column("ID_CP")]
        [Key]
        public int CPId { get; set; }

        [Column("CP")]
        public string CP { get; set; }

        [Column("COMMUNE")]
        public string Commune { get; set; }

        [Column("ID_DEPARTEMENT")]
        [ForeignKey("ParamDepartement")]
        public int DepartementId { get; set; }

        public virtual DbParamDepartement ParamDepartement { get; set; }
    }
}
