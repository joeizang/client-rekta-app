namespace RektaRetailApp.Web.Helpers
{
    public class PaginatedMetaData
    {
        public int TotalCount { get; }
        public int PageSize { get; }

        public int CurrentPage { get; }

        public string? PreviousPageLink { get; }

        public string? NextPageLink { get; }

        public PaginatedMetaData(int totalCount, int pageSize, int currentPage, string? previousPageLink, string? nextPageLink)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
        }

        public PaginatedMetaData()
        {
            
        }
    }
}
