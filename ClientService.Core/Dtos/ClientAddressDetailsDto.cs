using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class ClientAddressDetailsDto
    {
        public int IdClient { get; set; }
        public int? IdTypeAdresse { get; set; }
        public string? Adresse1 { get; set; }
        public string? Adresse2 { get; set; }
        public int? IdCp { get; set; }
        public int? IdPays { get; set; }
        public string? Telephone { get; set; }
        public string? Portable { get; set; }
        public int? NumVoie { get; set; }
        public string? Btqc { get; set; }
        public string? TypeVoie { get; set; }
        public string? TelephoneAutre { get; set; }
        public string? Fax { get; set; }
        public string? Batesc { get; set; }
        public string? Description { get; set; }
        public string? Nom { get; set; }
        public bool? ParDefaut { get; set; }
        public string? Cp { get; set; }
        public string? Commune { get; set; }
        public string? Bureau { get; set; }
    }
}
