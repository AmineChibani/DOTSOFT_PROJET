namespace ClientService.Core.Dtos
{
    public class CAResult
    {
        public DateTime? Fdate { get; set; }
        public long? IdFactureC { get; set; }  // Changed to long to match DB
        public string NumFacture { get; set; }  // Changed to string to match DB
        public string TypeFacture { get; set; }
        public decimal? Cattc { get; set; }
        public decimal? Achat { get; set; }
        public decimal? Caht { get; set; }
    }
}
