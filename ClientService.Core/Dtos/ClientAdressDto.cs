using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class ClientAdressesDto
    {

        public string? Address { get; set; }

        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        
    }
}
