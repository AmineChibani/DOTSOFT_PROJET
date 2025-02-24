﻿using ClientService.Core.Common;
using ClientService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Entities;
using ClientService.Core.Dtos;
using ClientService.Core.Specifications.Clients;
using ClientService.Core.Dtos;

namespace ClientService.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<DbClient>> GetClientById(int IdClient);
        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<List<DbClient>> GetClientsAsync();
        Task<DbClient> AddClient(DbClient client);

        Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<IEnumerable<CAResult>> GetCAAsync(CARequest request);
        Task<Result<List<DbParamCategSocioProf>>> GetCSPs();
    }
}