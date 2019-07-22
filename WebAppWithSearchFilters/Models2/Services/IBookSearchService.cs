using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models.BookData;

namespace WebAppWithSearchFilters.Models2.Services
{
    public interface IBookSearchService
    {
        List<Book> SearchBooks(
            out int totalCount,
            int pageSize,
            int pageNum,
            int[] author,
            int[] protagonist,
            int[] country,
            BookOrderBy orderBy = BookOrderBy.BookTitle,
            bool? orderDesc = null);
    }
}
