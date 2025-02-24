<<<<<<< HEAD
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
using ClientService.Core.Dtos;

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClients();
        Task<DbClient> AddClient(DbClient client);

        Task<Result<List<DbParamPays>>> GetAllPays();
        Task<IEnumerable<VenteResult>> GetVentesNationalesAsync(VenteRequest request);

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
        Task<IEnumerable<CAResult>> GetCAAsync(CARequest request);

    }
}
=======
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

namespace ClientService.Core.Interfaces
{
    public interface IClientService
    {
        Task<Result<DbClient>> GetClientById(int id);
        Task<List<DbClient>> GetClients();
        Task<Result<int>> Duplicate(int clientId, int adressTypeId);
        Task<DbClient> AddClient(DbClient client);

        Task<Result<List<DbParamPays>>> GetAllPays();

        Task<Result<List<ClientAddressDetailsDto>>> GetAddressesByClientId(int clientId);
    }
}
>>>>>>> cefeeb1fc6f3e47c423f66bbbe9bbadd67e7cec3
