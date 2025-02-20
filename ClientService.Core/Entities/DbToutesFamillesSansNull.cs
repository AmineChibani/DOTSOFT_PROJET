using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Entities
{
    [Table("TOUTESFAMILLES_SANSNULL")]
    public class DbToutesFamillesSansNull
    {
        [Column("ID_FAMILLE")]
        [Key]
        public int? ID_FAMILLE { get; set; }

        [Column("ID_COULEUR")]
        public int? ID_COULEUR { get; set; }

        [Column("IDRAYON")]
        public int? IDRAYON { get; set; }

        [Column("RAYON")]
        public string RAYON { get; set; }

        [Column("IDFAMILLE")]
        public int? IDFAMILLE { get; set; }

        [Column("FAMILLE")]
        public string FAMILLE { get; set; }

        [Column("IDSFAMILLE")]
        public int? IDSFAMILLE { get; set; }

        [Column("SFAMILLE")]
        public string SFAMILLE { get; set; }

        [Column("IDSSFAMILLE")]
        public int? IDSSFAMILLE { get; set; }

        [Column("SSFAMILLE")]
        public string SSFAMILLE { get; set; }

        [Column("COMPTE_TVA_RAYON")]
        public string COMPTE_TVA_RAYON { get; set; }

        [Column("COMPTE_TVA_FAMILLE")]
        public string COMPTE_TVA_FAMILLE { get; set; }

        [Column("COMPTE_TVA_SFAMILLE")]
        public string COMPTE_TVA_SFAMILLE { get; set; }

        [Column("COMPTE_TVA_SSFAMILLE")]
        public string COMPTE_TVA_SSFAMILLE { get; set; }

        [Column("FIDELITE_TYPE")]
        public string FIDELITE_TYPE { get; set; }

        [Column("FIDELITE_NB_PTS")]
        public int? FIDELITE_NB_PTS { get; set; }

        [Column("PAGE_MOU")]
        public string PAGE_MOU { get; set; }

        [Column("IMAGE")]
        public string IMAGE { get; set; }
    }
}
