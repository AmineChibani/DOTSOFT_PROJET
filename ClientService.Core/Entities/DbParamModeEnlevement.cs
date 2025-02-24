using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{

    [Table("PARAM_MODE_ENLEVEMENT")]
    public class DbParamModeEnlevement
    {
        [Column("ID_MODE_ENLEVEMENT")]
        public int? IdModeEnlevement { get; set; }

        [Column("NOM")]
        public string? Nom { get; set; }

        [Column("BL")]
        public bool? Bl { get; set; }

        [Column("FACTURE")]
        public bool? Facture { get; set; }

        [Column("TROC")]
        public bool? Troc { get; set; }

        [Column("COMMANDE")]
        public bool? Commande { get; set; }
    }

}
