using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{
    [Table("DROITS_SPECIAUX", Schema = "DOTSOFT")]
    public class DbDroitsSpeciaux
    {
        public string Droit { get; set; }
    }
}
