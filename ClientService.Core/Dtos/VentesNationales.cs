namespace ClientService.Core.Dtos
{
    public class VentesNationales
    {
        public string Nom { get; set; }
        public int IdStructure { get; set; }
        public string TypeAvoir { get; set; }
        public decimal Avoir { get; set; }
        public string NumFacture { get; set; }
        public int IdFacture { get; set; }
        public DateTime FDate { get; set; }
        public decimal MontantFacture { get; set; }
        public decimal Quantite { get; set; }
        public decimal MontantProduit { get; set; }
        public string NomProduit { get; set; }
        public decimal MontantAchat { get; set; }
        public decimal MontantTva { get; set; }
        public int IdClient { get; set; }
        public int IdProduit { get; set; }
        public string CodeReference { get; set; }
        public bool SansMarge { get; set; }
        public string TypeFacture { get; set; }
    }
}
