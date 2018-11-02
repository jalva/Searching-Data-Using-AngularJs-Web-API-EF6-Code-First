using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Filters
{
    public class SearchResultsResponse<T>
    {
        public IQueryable<T> Results { get; set; }
        public int TotalCount { get; set; }
    }
}