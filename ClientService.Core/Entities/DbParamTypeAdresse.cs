using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("PARAM_TYPE_ADRESSE", Schema = "DOTSOFT")]
    public class DbParamTypeAdresse
    {
        [Column("ID_TYPE_ADRESSE")]
        [Key]
        public int Id { get; set; }

        [Column("DESCRIPTION")]
        public string? Description;
    }
}
