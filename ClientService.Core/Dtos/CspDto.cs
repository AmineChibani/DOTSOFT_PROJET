using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class CspDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? CodeExterne { get; set; }
    }
}
