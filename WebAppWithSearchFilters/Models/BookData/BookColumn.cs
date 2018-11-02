using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.BookData
{
    public class BookColumn
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Field { get; set; }
        public string width { get; set; }
        public bool Sortable { get; set; }
        public string OrderBy { get; set; }
        public bool Visible { get; set; }
    }
}