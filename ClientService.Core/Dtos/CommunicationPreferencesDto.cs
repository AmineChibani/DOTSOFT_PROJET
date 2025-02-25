using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class CommunicationPreferencesDto
    {
        // Email preferences
        public bool OkPourMailing { get; set; }
        public bool OkPourMailingPartner { get; set; }
        public bool OkPourMailingAff { get; set; }

        // SMS preferences
        public bool OkPourSms { get; set; }
        public bool OkPourSmsPartner { get; set; }
        public bool OkPourSmsAff { get; set; }

        // Postal preferences
        public bool OkPourEnvoiPostal { get; set; }
        public bool OkPourEnvoiPostalPartner { get; set; }
        public bool OkPourEnvoiPostalAff { get; set; }
    }
}
