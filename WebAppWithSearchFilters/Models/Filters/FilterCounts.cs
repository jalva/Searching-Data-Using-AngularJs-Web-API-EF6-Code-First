using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Filters
{
    public class FilterCounts
    {
        public List<FilterTermCount> Author { get; set; }
        public List<FilterTermCount> Protagonist { get; set; }
        public List<FilterTermCount> Country { get; set; }
    }

    public class FilterTermCount
    {
        public string FilterTermId { get; set; }
        public int Count { get; set; }
    }
}