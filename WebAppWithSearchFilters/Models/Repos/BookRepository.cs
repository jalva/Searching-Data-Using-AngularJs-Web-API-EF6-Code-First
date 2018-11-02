using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebAppWithSearchFilters.Models.BookData;
using WebAppWithSearchFilters.Models.Filters;

namespace WebAppWithSearchFilters.Models
{
    public interface IBookRepository
    {
        IQueryable<Book> All(params Expression<Func<Book, object>>[] includeProperties);
        Book Find(int id);
        SearchResultsResponse<Book> Search(int[] author, int[] protagonist, int[] country, int? page = null, int? showCount = null, BookOrderBy? sortBy = null, bool? sortDesc = null);
    }

    public class BookRepository : IBookRepository, IDisposable
    {
        private BookContext _db = new BookContext();
        
        public IQueryable<Book> All(params Expression<Func<Book, object>>[] includeProperties)
        {
            return _search(null, null, null, includeProperties);
        }

        public Book Find(int id)
        {
            return _db.Books.Find(id);
        }

        public SearchResultsResponse<Book> Search(int[] author, int[] protagonist, int[] country, int? page, int? showCount, BookOrderBy? sortBy, bool? sortDesc)
        {
            var includePropsExpression = new Expression<Func<Book, object>>[]
            {
                b => b.Authors.Select(a => a.Country),
                b => b.Protagonists
            };

            // get the initial search queryable
            var bookQuery = _search(author, protagonist, country, includePropsExpression);

            var totalCount = bookQuery.Count();

            // add sorting
            var sortDescVal = sortDesc.GetValueOrDefault();
            switch (sortBy)
            {
                case BookOrderBy.BookTitle:
                    bookQuery = sortDescVal
                        ? bookQuery.OrderByDescending(b => b.Title)
                        : bookQuery.OrderBy(b => b.Title);
                    break;
                case BookOrderBy.BookId:
                default:
                    bookQuery = sortDescVal
                        ? bookQuery.OrderByDescending(b => b.BookId)
                        : bookQuery.OrderBy(b => b.BookId);
                    break;
            }

            // add pagination
            var pageVal = page.GetValueOrDefault(-1);
            var showCountVal = showCount.GetValueOrDefault(5);
            if (pageVal > -1)
            {
                bookQuery = bookQuery.Skip(pageVal * showCountVal).Take(showCountVal);
            }

            var result = new SearchResultsResponse<Book>
            {
                Results = bookQuery,
                TotalCount = totalCount
            };

            return result;
        }


        private IQueryable<Book> _search(
            int[] author,
            int[] protagonist, 
            int[] country,
            params Expression<Func<Book, object>>[] includeProperties)
        {
            var query = _db.Books.AsQueryable();

            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (author != null && author.Any() && author.All(p => p > 0))
            {
                query = query.Where(b => author.Any(a => b.Authors.Any(a2 => a2.AuthorId == a)));
            }

            if (protagonist != null && protagonist.Any() && protagonist.All(p => p > 0))
            {
                query = query.Where(b => protagonist.Any(p => b.Protagonists.Any(p2 => p2.ProtagonistId == p)));
            }

            if (country != null && country.Any() && country.All(p => p > 0))
            {
                query = query.Where(b => country.Any(c => b.Authors.Any(a => a.CountryId == c)));
            }


            return query;
        }
        

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}