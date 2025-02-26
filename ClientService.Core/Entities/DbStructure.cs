using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{

    [Table("STRUCTURE", Schema = "DOTSOFT")]
    public class DbStructure
    {
        [Key]
        [Column("ID_STRUCTURE")]
        public int IdStructure { get; set; }

        [Column("NOM")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Column("ENSEIGNE")]
        [StringLength(50)]
        public string Enseigne { get; set; }

        [Column("ID_DISTRIB")]
        [DefaultValue(0)]
        [Precision(22,18)]
        public int Id_Distrib { get; set; }

        [Column("ID_PAYS")]
        public int id_pays { get; set; }
    }

}
