using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientService.Core.Entities
{
    [Table("CLIENT_OPTIN", Schema = "DOTSOFT")]
    public class DbClientOptin
    {
        [Key]
        [Column("ID_CLIENT")]
        public int ClientId { get; set; }

        [Column("DATE_OPTIN_SMS")]
        public DateTime? DateOptinSms { get; set; }

        [Column("DATE_AFF_OPTIN_SMS")]
        public DateTime? DateAffOptinSms { get; set; }

        [Column("DATE_PARTNER_OPTIN_SMS")]
        public DateTime? DatePartnerOptinSms { get; set; }

        [Column("DATE_OPTIN_EMAIL")]
        public DateTime? DateOptinEmail { get; set; }

        [Column("DATE_AFF_OPTIN_EMAIL")]
        public DateTime? DateAffOptinEmail { get; set; }

        [Column("DATE_PARTNER_OPTIN_EMAIL")]
        public DateTime? DatePartnerOptinEmail { get; set; }

        [Column("DATE_OPTIN_POSTAL")]
        public DateTime? DateOptinPostal { get; set; }

        [Column("DATE_AFF_OPTIN_POSTAL")]
        public DateTime? DateAffOptinPostal { get; set; }

        [Column("DATE_PARTNER_OPTIN_POSTAL")]
        public DateTime? DatePartnerOptinPostal { get; set; }

        public DbClient Client { get; set; }
    }
}
