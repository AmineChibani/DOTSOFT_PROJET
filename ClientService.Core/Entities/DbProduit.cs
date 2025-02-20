using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("Produit")]
    public class DbProduit
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        [Column("CodeProduit")]
        public string CodeProduit { get; set; }

        [Required, MaxLength(255)]
        [Column("Designation")]
        public string Designation { get; set; }

        [Column("Type")]
        public int? Type { get; set; }

        [Column("Categorie")]
        public int? Categorie { get; set; }

        [Column("IdFamille")]
        public int? IdFamille { get; set; }

        [MaxLength(50)]
        [Column("CompteVente")]
        public string CompteVente { get; set; }

        [MaxLength(50)]
        [Column("CompteAchat")]
        public string CompteAchat { get; set; }

        [MaxLength(50)]
        [Column("CodeBarre")]
        public string CodeBarre { get; set; }

        [Column("StockActuel")]
        public decimal? StockActuel { get; set; }

        [Column("StockMin")]
        public decimal? StockMin { get; set; }

        [Column("StockMax")]
        public decimal? StockMax { get; set; }

        [Column("PrixAchatHT")]
        public decimal? PrixAchatHT { get; set; }

        [Column("PrixVenteHT")]
        public decimal? PrixVenteHT { get; set; }

        [Column("PrixVenteTTC")]
        public decimal? PrixVenteTTC { get; set; }

        [Column("TVA")]
        public decimal? TVA { get; set; }

        [Column("IdUnite")]
        public int? IdUnite { get; set; }

        [MaxLength(50)]
        [Column("RefFournisseur")]
        public string RefFournisseur { get; set; }

        [Column("IdFournisseur")]
        public int? IdFournisseur { get; set; }

        [Column("IdMarque")]
        public int? IdMarque { get; set; }

        [Column("IdModele")]
        public int? IdModele { get; set; }

        [Column("IdCouleur")]
        public int? IdCouleur { get; set; }

        [Column("IdTaille")]
        public int? IdTaille { get; set; }

        [Column("IdEmplacement")]
        public int? IdEmplacement { get; set; }

        [Column("Perissable")]
        public bool? Perissable { get; set; }

        [Column("IdTVA")]
        public int? IdTVA { get; set; }

        [Column("EstActif")]
        public bool? EstActif { get; set; }

        [Column("IdDevise")]
        public int? IdDevise { get; set; }

        [MaxLength(255)]
        [Column("Image")]
        public string Image { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Column("DateAjout", TypeName = "datetime")]
        public DateTime? DateAjout { get; set; }

        [Column("IdUtilisateur")]
        public int? IdUtilisateur { get; set; }
    }
}
