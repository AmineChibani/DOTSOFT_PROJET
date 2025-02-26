namespace ClientService.Core.Dtos
{
    namespace ClientService.Core.Dtos
    {
        public class AvoirResult
        {
            public int ID_BA { get; set; }
            public int? ID_STRUCTURE { get; set; }
            public int? ID_EMPLOYE { get; set; }
            public DateTime? FDATE { get; set; }
            public DateTime? DATE_DEBUT { get; set; }
            public DateTime? DATE_FIN { get; set; }
            public int? ID_CLIENT { get; set; }
            public string? CODE { get; set; }  
            public int? ANNULE { get; set; }
            public decimal? MONTANT_BA_INITIAL { get; set; }
            public decimal? MONTANT_BA_VALIDE { get; set; }
            public decimal? MONTANT_BA_ENCOURS { get; set; }
            public int? TYPE_BA { get; set; }
            public DateTime? DATE_ANNULATION { get; set; }
            public int? TYPE_ANNULATION { get; set; }
            public int? ID_EMPLOYE_ANNULE { get; set; }
            public int? TYPE_REGLEMENT_CMD { get; set; }
            public int? ID_COMMANDEC { get; set; }
            public int? VALIDITE_BA_MOIS { get; set; }
            public int? ID_FACTUREC { get; set; }
            public decimal? TAUX_TVA { get; set; }
            public int? ID_DISTRIB { get; set; }
            public string? CODE_BARRE_COMPLET { get; set; }  
            public string? DESTINATAIRE { get; set; }  
            public string? MESSAGE { get; set; }  
            public int? ID_DEVISE { get; set; }
            public decimal? montant_BA_restant { get; set; }
            public long? ID_TICKET_CAISSE { get; set; }
            public int? num_commande { get; set; }
        }
    }
}
