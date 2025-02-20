using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ClientService.Core.Entities
{
    [Table("CLIENT_ADRESSE", Schema = "DOTSOFT")]
    public class DbClientAdresse
    {
        [Key]
        [Column("ID_CLIENT")]
        [Required]
        public int ClientId { get; set; }

        [Column("ID_TYPE_ADRESSE")]
        [Required]
        public int AdresseTypeId { get; set; }

        [Column("ADRESSE1")]
        public string? Adresse1 { get; set; }

        [Column("ADRESSE2")]
        public string? Adresse2 { get; set; }

        [Column("ID_CP")]
        [ForeignKey("ParamCodePostal")]
        public int? CpId { get; set; }

        [Column("ID_PAYS")]
        [ForeignKey("Pays")]
        public int? PaysId { get; set; }

        [Column("TELEPHONE")]
        public string? PhoneNumber { get; set; }

        [Column("PORTABLE")]
        public string? CellPhone { get; set; }

        [Column("ABANDON")]
        public int? Abandon { get; set; }

        [Column("CP_ETRANGER")]
        public string? CpEtranger { get; set; }

        [Column("VILLE_ETRANGER")]
        public string? VilleEtranger { get; set; }

        [Column("NUM_VOIE")]
        public int? NumVoie { get; set; }

        [Column("BTQC")]
        public string? Btqc { get; set; }

        [Column("TYPE_VOIE")]
        public string? TypeVoie { get; set; }

        [Column("FAX")]
        public string? Fax { get; set; }

        [Column("BATESC")]
        public string? Batesc { get; set; }

        [Column("TELEPHONE_AUTRE")]
        public string? TelephoneAutre { get; set; }

        public virtual DbParamPays Pays { get; set; }

        public virtual DbClient Client { get; set; }

        public virtual DbParamCodePostal ParamCodePostal { get; set; }
    }
}
