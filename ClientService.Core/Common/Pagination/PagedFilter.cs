namespace ClientService.Core.Common.Pagination
{
    public class PagedFilter
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? ColumnsSearchText { get; set; }

        public int OrderByColumn { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
