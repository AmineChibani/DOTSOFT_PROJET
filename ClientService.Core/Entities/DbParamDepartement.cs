using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_DEPARTEMENT", Schema = "DOTSOFT")]
    public class DbParamDepartement
    {
        [Column("ID_DEPARTEMENT")]
        [Key]
        public int DepartementId { get; set; }

        [Column("ID_REGION")]
        [ForeignKey("ParamRegion")]
        public int RegionId { get; set; }

        [Column("LIBELLE")]
        public string Libelle { get; set; }

        [Column("CODE_POSTAL")]
        public int CpId { get; set; }

        public virtual DbParamRegion ParamRegion { get; set; }
    }
}
