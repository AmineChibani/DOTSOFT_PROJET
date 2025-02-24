using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Dtos
{
    public class VenteResult
    {
        public string Nom { get; set; }

        public int Id_Structure { get; set; }

        public string Type_Avoir { get; set; }

        public decimal Avoir { get; set; }

        public int Num_Facture { get; set; }

        public int Id_Facture { get; set; }

        public DateTime? FDate { get; set; }

        public decimal Montant_Facture { get; set; }

        public decimal Quantite { get; set; }

        public decimal Montant_Produit { get; set; }

        public string Nom_Produit { get; set; }

        public decimal Montant_Achat { get; set; }

        public decimal Montant_Tva { get; set; }

        public int Id_Client { get; set; }

        public int Id_Produit { get; set; }

        public string Code_Reference { get; set; }

        public int Sans_Marge { get; set; }

        public string Type_Facture { get; set; }
    }
}