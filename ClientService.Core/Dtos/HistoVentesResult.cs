namespace ClientService.Core.Dtos
{
    public class HistoVentesResult
    {
        public long? IdMessage { get; set; }
        public long? IdCommandec { get; set; }
        public string? NumCommande { get; set; }
        public DateTime? DateCmd { get; set; }
        public string? NumFacture { get; set; }
        public int? TypeAvoir { get; set; }
        public string? CodeReference { get; set; }
        public int? Avoir { get; set; }
        public long? IdFacture { get; set; }
        public DateTime? Fdate { get; set; }
        public decimal? MontantFacture { get; set; }
        public string? SerieFacture { get; set; }
        public int? Quantite { get; set; }
        public decimal? MontantProduit { get; set; }
        public decimal? MontantTotal { get; set; }
        public string? NomProduit { get; set; }
        public long? IdFournisseur { get; set; }
        public long? IdBordereau { get; set; }
        public decimal? MontantAchat { get; set; }
        public decimal? MontantTva { get; set; }
        public long? IdClient { get; set; }
        public long? IdProduit { get; set; }
        public int? SansMarge { get; set; }
        public string? TypeFacture { get; set; }
        public string? Marque { get; set; }
        public long? IdMarque { get; set; }
        public string? Rayon { get; set; }
        public string? Famille { get; set; }
        public long? IdCouleur { get; set; }
        public long? IdTypeProduit { get; set; }
        public string? NumFactureOrigine { get; set; }
        public string? Sfamille { get; set; }
        public long? IdFamille { get; set; }
        public int? DossierSav { get; set; }
        public long? ProdSav { get; set; }
        public long? IdDossier { get; set; }
        public string? TrocAxe1 { get; set; }
        public string? TrocAxe2 { get; set; }
        public int? CreationPossible { get; set; }
        public long? IdDevise { get; set; }
    }

}
