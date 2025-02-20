using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{
    [Table("MARQUE", Schema = "DOTSOFT")]
    public class DbMarque
    {
        [Key]
        [Column("ID_MARQUE")]
        public int Id_Marque { get; set; }

        [Column("CODE")]
        public string? Code { get; set; }

        [Column("NOM")]
        public string? Nom { get; set; }

        [Column("ADRESSE1")]
        public string? Adresse1 { get; set; }

        [Column("ADRESSE2")]
        public string? Adresse2 { get; set; }

        [Column("ID_CP")]
        public int? Id_Cp { get; set; }

        [Column("ID_PAYS")]
        public int? Id_Pays { get; set; }

        [Column("TEL")]
        public string? Tel { get; set; }

        [Column("FAX")]
        public string Fax { get; set; }

        [Column("MAIL")]
        public string? Mail { get; set; }

        [Column("ABANDON")]
        public int? Abondon { get; set; }

        [Column("REACTIVATION")]
        public int? Reactivation { get; set; }

        [Column("GARANTIE")]
        public string? Garantie { get; set; }

        [Column("ID_STRUCTURE")]
        public int? Id_Structure { get; set; }

        [Column("DATE_ABANDON")]
        public DateTime? Date_Abondon { get; set; }

        [Column("ID_EMPLOYE_ABANDON")]
        public int? Id_Employe_Abondon { get; set; }

        [Column("IMAGE")]
        public string? Image { get; set; }

        [Column("IMAGE_TYPEMIME")]
        public string? Image_TypeMime { get; set; }

        [Column("NOM_IMAGE")]
        public string? Nom_Image { get; set; }

        [Column("ID_SOCIETE_C")]
        public int? Id_Societe_C { get; set; }

        [Column("ENFANT_OU_ADULTE")]
        public string? Enfant_Ou_Adulte { get; set; }

        [Column("TXT_IDEN")]
        public string? Txt_Iden { get; set; }

        [Column("DUREE_GARANTIE_CONSTRUCTEUR")]
        public int? Duree_Garantie_Constructeur { get; set; }

        [Column("TYPE_GARANTIE_CONSTRUCTEUR")]
        public string? Type_Grantie_Constructeur { get; set; }

        [Column("HORS_POLITIQUE_TARIFAIRE")]
        public int? Hors_Politique_Traifiare { get; set; }
    }
}
