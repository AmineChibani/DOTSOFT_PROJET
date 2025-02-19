using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ClientService.Core.Entities
{
    [Table("CLIENT_TYPE", Schema = "DOTSOFT")]
    public class DbClientType
    {
        [Key]
        [Column("ID_TYPE_CLIENT")]
        public int IdTypeClient { get; set; }

        [Column("LIBELLE")]
        public string Libelle { get; set; }
        [Required]
        [Column("MARGE")]
        [DefaultValue(0)]
        [Precision(22, 1)]  
        public decimal Marge { get; set; }

       public List<DbClient> Clients { get; set; }
    }
}
