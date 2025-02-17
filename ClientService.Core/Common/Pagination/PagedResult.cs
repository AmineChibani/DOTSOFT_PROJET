namespace ClientService.Core.Common.Pagination
{
    public class PagedResult<T>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int PageCount { get; private set; }

        public int RecordsCount { get; private set; }

        public int AllRecordsCount { get; private set; }

        public IList<T> Results { get; set; }

        public PagedResult(int pageNumber, int pageSize, int recordsCount, int allRecordsCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.RecordsCount = recordsCount;
            this.AllRecordsCount = allRecordsCount;
            this.PageCount = recordsCount > 0 ? (int)Math.Ceiling((double)recordsCount / (double)pageSize) : 0;
            this.Results = (IList<T>)new List<T>();
        }

        public PagedResult() => throw new NotImplementedException();
    }
}
