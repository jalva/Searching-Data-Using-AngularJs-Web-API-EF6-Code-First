using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Filters
{
    public class SearchFilter
    {
        public string FilterName { get; set; }
        public string FilterQsKey { get; set; }
        public List<FilterTerm> FilterTerms { get; set; }

    }

    public class FilterTerm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
        public bool Selected { get; set; }
        public List<SearchFilter> NestedFilters { get; set; }
        public bool IsAllOption { get; set; }
    }
}