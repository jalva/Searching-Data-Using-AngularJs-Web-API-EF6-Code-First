using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models.BookData;
using WebAppWithSearchFilters.Models2.Repositories;
using WebAppWithSearchFilters.Models2.UnitOfWork;

namespace WebAppWithSearchFilters.Models2.Services
{
    public class BookSearchService : IBookSearchService
    {
        private GenericRepository<Book> _repo;
        private IMyUnitOfWork _unitOfWork;

        public BookSearchService(IMyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.BookRepo;
        }

        public List<Book> SearchBooks(
            out int totalCount, 
            int pageSize, 
            int pageNum, 
            int[] author, 
            int[] protagonist, 
            int[] country, 
            BookOrderBy orderBy = BookOrderBy.BookTitle, 
            bool? orderDesc = default(bool?))
        {
            // included entities
            var includedEntities = new List<Expression<Func<Book, object>>>
            {
                b => b.Authors.Select(a => a.Country),
                b => b.Protagonists
            };

            // search filters
            var searchCriteria = new List<Expression<Func<Book, bool>>>();
            
            if (author != null && author.Any() && author.All(p => p > 0))
            {
                searchCriteria.Add(b => author.Any(a => b.Authors.Any(a2 => a2.AuthorId == a)));
            }

            if (protagonist != null && protagonist.Any() && protagonist.All(p => p > 0))
            {
                searchCriteria.Add(b => protagonist.Any(p => b.Protagonists.Any(p2 => p2.ProtagonistId == p)));
            }

            if (country != null && country.Any() && country.All(p => p > 0))
            {
                searchCriteria.Add(b => country.Any(c => b.Authors.Any(a => a.CountryId == c)));
            }

            // order by
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderByFunc = GetOrderBy(orderBy, orderDesc.GetValueOrDefault());

            var bookSearchQuery = this._repo.Get(searchCriteria, orderByFunc, includedEntities);

            totalCount = bookSearchQuery.Count();

            bookSearchQuery = bookSearchQuery.Skip(pageNum * pageSize).Take(pageSize);

            return bookSearchQuery.AsNoTracking().ToList();
        }


        private Func<IQueryable<Book>, IOrderedQueryable<Book>> GetOrderBy(BookOrderBy orderBy, bool orderDesc)
        {
            switch (orderBy)
            {
                case BookOrderBy.BookId:
                    return query => !orderDesc ? query.OrderBy(b => b.BookId) : query.OrderByDescending(b => b.BookId);
                case BookOrderBy.BookTitle:
                default:
                    return query => !orderDesc ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title);
            }
        }
    }
}