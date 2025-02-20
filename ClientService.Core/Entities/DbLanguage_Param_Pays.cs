using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{
    [Table("LANGUAGE_PARAM_PAYS", Schema = "DOTSOFT")]
    public class DbLanguage_Param_Pays
    {
        [Key]
        [Column("ID_PAYS")]
        public int Id_Pays { get; set; }

        [Column("CODE")]
        public int? Code { get; set; }

        [Column("NOM")]
        public string? Nom { get; set; }
    }
}
