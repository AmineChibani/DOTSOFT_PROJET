using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{
    [Table("CLIENT_ADRESSE_COMPLEMENT", Schema = "DOTSOFT")]
    public class DbClientAdresseComplement
    {
        [Key]
        [Column("ID_CLIENT")]
        [Required]
        public int ClientId { get; set; }

        [Column("ID_TYPE_ADRESSE")]
        [Required]
        public int AdresseTypeId { get; set; }

        [Column("OK_POUR_ENVOI_POSTAL")]
        public int? OkPourEnvoiPostal { get; set; }

        [Column("OK_POUR_ENVOI_POSTAL_PARTNER")]
        public int? OkPourEnvoiPostalPartner { get; set; }

        [Column("OK_POUR_ENVOI_POSTAL_AFF")]
        public int? OkPourEnvoiPostalAff { get; set; }

        public virtual DbClient Client { get; set; }
    }
}
