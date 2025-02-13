using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.WebAPI.Dtos
{
    public class CA
    {
        public DateTime? Fdate { get; set; }

        public int? IdFactureC { get; set; }

        public int? NumFacture { get; set; }

        public string TypeFacture { get; set; }

        public Decimal? Cattc { get; set; }

        public Decimal? Achat { get; set; }

        public Decimal? Caht { get; set; }
    }
}
