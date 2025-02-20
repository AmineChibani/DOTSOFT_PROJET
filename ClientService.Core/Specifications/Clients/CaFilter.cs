using ClientService.Core.Entities;
using System.Linq.Expressions;

namespace ClientService.Core.Specifications.Clients
{
    public class CaFilter
    {
        public int ClientId { get; set; }

        public int StructureId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool All { get; set; }

        internal List<Expression<Func<DbClientFacture, bool>>> ToWhereClauses()
        {
            List<Expression<Func<DbClientFacture, bool>>> expressionList = new List<Expression<Func<DbClientFacture, bool>>>();
            expressionList.Add((Expression<Func<DbClientFacture, bool>>)(x => x.IdClient == this.ClientId));
            List<Expression<Func<DbClientFacture, bool>>> whereClauses = expressionList;
            if (this.StructureId != 0)
                whereClauses.Add((Expression<Func<DbClientFacture, bool>>)(x => x.IdStructure == (int?)this.StructureId));
            if (!this.All)
                whereClauses.Add((Expression<Func<DbClientFacture, bool>>)(x => (DateTime?)x.Fdate >= this.StartDate && (DateTime?)x.Fdate <= this.EndDate));
            return whereClauses;
        }
    }
}

