using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Entities
{
    [Table("BORDEREAU_ENTREE", Schema = "DOTSOFT")]
    public class DbBordereau_Entree
    {
        [Key]
        [Column("ID_BORDEREAU")]
        public int Id_Bordereau { get; set; }

        [Column("ID_STRUCTURE")]
        public int? Id_Structure { get; set; }

        [Column("ID_BL_FOURNISSEUR")]
        public string? Id_Bl_Fournisseur { get; set; }

        [Column("FDATE")]
        public DateTime? FDate { get; set; }

        [Column("ID_COMMANDEF")]
        public int? Id_Commande { get; set; }

        [Column("ID_FOURNISSEUR")]
        public int? Id_Fournisseur { get; set; }

        [Column("TYPE_FOURNISSEUR")]
        public string? Type_Fournisseur { get; set; }

        [Column("LOT")]
        public int? Lot { get; set; }


        [Column("ID_EMPLOYE")]
        public int? Id_Employee { get; set; }

        [Column("ID_CAISSE")]
        public int? Id_Caisse { get; set; }

        [Column("ANNULABLE")]
        public int? Annulable { get; set; }

        [Column("DEPRECIE")]
        public int? Deprecie { get; set; }

        [Column("NUM_ECART")]
        public string? Num_Ecart { get; set; }

        [Column("MOTIFS")]
        public string? Motifs { get; set; }

        [Column("ES_MANUELLE")]
        public int? Es_Manuelle { get; set; }

        [Column("NUM_BE")]
        public string? Num_Be { get; set; }

        [Column("ID_MOTIF")]
        public int? Id_Motif { get; set; }
    }
}
