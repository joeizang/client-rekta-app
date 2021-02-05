using System;
using System.Collections;
using System.Collections.Generic;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.ViewModel
{
    public class PaginatedResponse<T> where T : class
    {
        public IEnumerable<T> Data { get; }

        public int TotalCount { get; }
        public int PageSize { get; }

        public int CurrentPage { get; }

        public string? PreviousPageLink { get; private set; }

        public string? NextPageLink { get; private set; }

        public string CurrentResponseStatus { get; }

        public List<object>? Error { get; set; }

        public void SetNavLinks(string? previousLink, string? nextLink)
        {
            if (string.IsNullOrEmpty(previousLink) || string.IsNullOrEmpty(nextLink))
            {
                PreviousPageLink = string.Empty;
                NextPageLink = string.Empty;
            }
            else
            {
                PreviousPageLink = previousLink;
                NextPageLink = nextLink;
            }
        }


        public PaginatedResponse(IEnumerable<T> sequence, int totalCount, int pageSize, int currentPage, string? previousPageLink, 
            string? nextPageLink, string currentResponseStatus, object? errors = null)
        {
            Data = sequence;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
            CurrentResponseStatus = currentResponseStatus;
            if(errors != null)
                Error?.Add(errors);
        }

        public PaginatedResponse(PagedList<T> list, string currentResponseStatus, dynamic? errors = null)
        {
            Data = list;
            CurrentResponseStatus = currentResponseStatus;
            if(typeof(IEnumerable).IsAssignableFrom(errors) && errors != null)
                Error?.AddRange(errors);
            Error?.Add(errors);
        }

        public PaginatedResponse()
        {
            Data = new List<T>();
            CurrentResponseStatus = ResponseStatus.NonAction;
        }
    }
}
