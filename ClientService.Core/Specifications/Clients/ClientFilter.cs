using ClientService.Core.Common.Pagination;
using ClientService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Core.Specifications.Clients
{
    public class ClientFilter : PagedFilter
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public int? ClientId { get; set; }
        public string? Phone { get; set; }
        public string? Postal { get; set; }
        public int? Pays { get; set; }
        public int? Region { get; set; }
        public int? StructureId { get; set; }
        public int? EmployeId { get; set; }

        public List<Expression<Func<DbClient, bool>>> ToWhereClauses()
        {
            var whereClauses = new List<Expression<Func<DbClient, bool>>>();

            if (StructureId.HasValue)
                whereClauses.Add(x => x.StructureOriginId == StructureId.Value);

            if (!string.IsNullOrWhiteSpace(Name))
                whereClauses.Add(x => x.FirstLastName.Contains(Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(Email))
                whereClauses.Add(x => x.Mail.Equals(Email, StringComparison.OrdinalIgnoreCase));

            if (ClientId.HasValue)
                whereClauses.Add(x => x.ClientId == ClientId.Value);

            if (!string.IsNullOrWhiteSpace(City))
                whereClauses.Add(x => x.ClientAdresses.Any(y => y.ParamCodePostal.Commune.Contains(City, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrWhiteSpace(Phone))
                whereClauses.Add(x => x.ClientAdresses.Any(y => y.PhoneNumber.Contains(Phone, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrWhiteSpace(Postal))
                whereClauses.Add(x => x.ClientAdresses.Any(y => y.ParamCodePostal.CP.Contains(Postal, StringComparison.OrdinalIgnoreCase)));

            if (Pays.HasValue)
                whereClauses.Add(x => x.ClientAdresses.Any(y => y.Pays.PaysId == Pays.Value));

            if (Region.HasValue)
                whereClauses.Add(x => x.ClientAdresses.Any(y => y.ParamCodePostal.ParamDepartement.RegionId == Region.Value));

            return whereClauses;
        }

        public IQueryable<DbClient> ApplySorting(IQueryable<DbClient> query, int sortColumn, SortDirection sortDirection)
        {
            bool ascending = sortDirection == SortDirection.Ascending;

            query = sortColumn switch
            {
                1 => ascending ? query.OrderBy(x => x.Nom) : query.OrderByDescending(x => x.Nom),
                2 => ascending ? query.OrderBy(x => x.Prenom) : query.OrderByDescending(x => x.Prenom),
                3 => ascending ? query.OrderBy(x => x.ClientAdresses.FirstOrDefault().Adresse1)
                               : query.OrderByDescending(x => x.ClientAdresses.FirstOrDefault().Adresse1),
                4 => ascending ? query.OrderBy(x => x.ClientAdresses.FirstOrDefault().Pays.Libelle)
                               : query.OrderByDescending(x => x.ClientAdresses.FirstOrDefault().Pays.Libelle),
                5 => ascending ? query.OrderBy(x => x.ClientAdresses.FirstOrDefault().ParamCodePostal.ParamDepartement.ParamRegion.Libelle)
                               : query.OrderByDescending(x => x.ClientAdresses.FirstOrDefault().ParamCodePostal.ParamDepartement.ParamRegion.Libelle),
                _ => ascending ? query.OrderBy(x => x.Nom) : query.OrderByDescending(x => x.Nom)
            };

            return query;
        }
    }
}

