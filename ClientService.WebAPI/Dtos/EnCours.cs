using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.WebAPI.Dtos
{
    public class EnCours
    {
        public int IdFactureC { get; set; }

        public int IdLigne { get; set; }

        public int IdProduit { get; set; }

        public int? Quantite { get; set; }

        public string TypeFacture { get; set; }

        public string Famille { get; set; }

        public int? NumFacture { get; set; }

        public int IdClient { get; set; }

        public int? Etat { get; set; }

        public DateTime? EtatDate { get; set; }

        public string EtatHistorique { get; set; }

        public string NomMarque { get; set; }

        public string CodeReference { get; set; }

        public string ModeEnlevement { get; set; }

        public int? DateJour { get; set; }

        public int? DateMois { get; set; }

        public int? DateAnnee { get; set; }
    }
}
