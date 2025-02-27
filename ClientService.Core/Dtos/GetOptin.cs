using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Dtos
{
    public class GetOptinBaseDto
    {
        public bool OkPourMailing { get; set; }
        public bool OkPourMailingPartner { get; set; }
        public bool OkPourSms { get; set; }
        public bool OkPourSmsPartner { get; set; }
        public bool OkPourEnvoiPostal { get; set; }
        public bool OkPourEnvoiPostalPartner { get; set; }

        public GetOptinBaseDto() { }
        public GetOptinBaseDto(GetOptinBaseDto baseDto)
        {
            OkPourMailing = baseDto.OkPourMailing;
            OkPourMailingPartner = baseDto.OkPourMailingPartner;
            OkPourSms = baseDto.OkPourSms;
            OkPourSmsPartner = baseDto.OkPourSmsPartner;
            OkPourEnvoiPostal = baseDto.OkPourEnvoiPostal;
            OkPourEnvoiPostalPartner = baseDto.OkPourEnvoiPostalPartner;
        }
    }

    public class GetOptinFullDto : GetOptinBaseDto
    {
        public bool OkPourMailingAff { get; set; }
        public bool OkPourSmsAff { get; set; }
        public bool OkPourEnvoiPostalAff { get; set; }

        public GetOptinFullDto() { }
        public GetOptinFullDto(GetOptinBaseDto baseDto)
            : base(baseDto) { }
    }

}
