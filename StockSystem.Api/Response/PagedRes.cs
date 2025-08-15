namespace StockSystem.Api.Response;

public class PagedRes 
{
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage { get; set; }
}