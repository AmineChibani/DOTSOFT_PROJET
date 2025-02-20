using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{
    [Table("ECOMMERCE_DEMANDES", Schema = "DOTSOFT")]
    public class DbEcommerce_Demandes
    {
        [Key]
        [Column("ID_MESSAGE")]
        public int Id_Message { get; set; }

        [Column("ID_CLIENT")]
        public int? Id_Client { get; set; }

        [Column("ID_COMMANDEC")]
        public long? Id_CommandeC { get; set; }

        [Column("MESSAGE_CLIENT")]
        public string? Message_Client { get; set; }

        [Column("MESSAGE_INTERNE")]
        public string? Message_Intern { get; set; }

        [Column("FDATE")]
        public DateTime? Fdate { get; set; }

        [Column("FDATE_REPONSE")]
        public DateTime? FdateReponse { get; set; }

        [Column("FDATE_REPONSE_HL")]
        public DateTime? FdateReponse_Hl { get; set; }

        [Column("CLOTURE")]
        public int? Cloture { get; set; }

        [Column("ID_CANAL")]
        public int? Id_Canal { get; set; }

        [Column("ID_QUALIFICATION")]
        public int? Id_Qualification { get; set; }

        [Column("VISIBLE")]
        public int? Visible { get; set; }

        [Column("ID_EMPLOYE")]
        public int? Id_Employe { get; set; }

        [Column("REQUALIFICATION")]
        public int? Requalification { get; set; }

        [Column("ID_DOSSIER")]
        public int? Id_Dossier { get; set; }
    }

}
