using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class ClientAddressDetailsDto
    {
        public int id_client { get; set; }
        public int? id_type_adresse { get; set; }
        public string? adresse1 { get; set; }
        public string? adresse2 { get; set; }
        public int? id_cp { get; set; }
        public int? id_pays { get; set; }
        public string? telephone { get; set; }
        public string? portable { get; set; }
        public int? num_voie { get; set; }
        public string? btqc { get; set; }
        public string? type_voie { get; set; }
        public string? telephone_autre { get; set; }
        public string? fax { get; set; }
        public string? batesc { get; set; }
        public string? description { get; set; }
        public string? nom { get; set; }
        public bool? par_defaut { get; set; }
        public string? cp { get; set; }
        public string? commune { get; set; }
        public string? bureau { get; set; }
        public bool eticket { get; set; }
        public bool NPAI { get; set; }

        public string libelle { get; set; }
    }
}
