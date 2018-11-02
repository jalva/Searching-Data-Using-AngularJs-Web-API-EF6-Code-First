using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Dtos
{

    public class SearchResponseDto<T>
    {
        public List<T> Results { get; set; }
        public int TotalCount { get; set; }
    }
}