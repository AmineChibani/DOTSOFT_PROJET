namespace ClientService.Core.Dtos
{
    public class CAResult
    {
        public DateTime? Fdate { get; set; }
        public long? IdFactureC { get; set; }  
        public long? NumFacture { get; set; }  
        public string TypeFacture { get; set; }
        public decimal? Cattc { get; set; }
        public decimal? Achat { get; set; }
        public decimal? Caht { get; set; }
    }
}
