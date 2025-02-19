namespace ClientService.Core.Dtos
{
    public class Avoirs
    {
        public DateTime? FdateFacture { get; set; }

        public DateTime? FdateAvoir { get; set; }

        public DateTime? DateAnnulation { get; set; }

        public DateTime? DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public int? IdFactureC { get; set; }

        public int? IdAvoirC { get; set; }

        public int? NumFacture { get; set; }

        public int? NumAvoir { get; set; }

        public int? IdAvoir { get; set; }

        public Decimal? Cattc { get; set; }

        public Decimal? Caht { get; set; }

        public Decimal? Achat { get; set; }

        public string TypeAvoir { get; set; }

        public string TypeFacture { get; set; }

        public int? IdTicketCaisse { get; set; }

        public Decimal? MontantBaRestant { get; set; }

        public int? NumCommande { get; set; }

        public string Code { get; set; }
    }
}
