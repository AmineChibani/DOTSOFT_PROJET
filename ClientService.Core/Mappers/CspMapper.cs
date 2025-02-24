using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Dtos;
using ClientService.Core.Entities;

namespace ClientService.Core.Mappers
{
    public static class CspMapper
    {
        public static CspDto toCspDto(this DbParamCategSocioProf data)
        {
            return new CspDto
            {
                Id = data.IdCsp,
                Nom = data.Nom,
                CodeExterne = data.CodeExterne,
            };
        }
    }
}
