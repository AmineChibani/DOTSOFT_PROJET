using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_REGION", Schema = "DOTSOFT")]
    public class DbParamRegion
    {
        [Column("ID_REGION")]
        [Key]
        public int RegionId { get; set; }

        [Column("LIBELLE")]
        public string Libelle { get; set; }

        [Column("ID_PAYS")]
        public int PaysId { get; set; }
    }
}
