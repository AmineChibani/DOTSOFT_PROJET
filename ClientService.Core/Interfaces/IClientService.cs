﻿using ClientService.Core.Common;
using ClientService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientService.Core.Common;
using ClientService.Core.Entities;

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClients();
        Task<DbClient> AddClient(DbClient client);

        Task<Result<List<DbParamPays>>> GetAllPays();

        Task<Result<List<DbClientAdresse>>> GetAddressByClientId(int clientId);
    }
}
