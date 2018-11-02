using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebAppWithSearchFilters.Models.Filters;

namespace WebAppWithSearchFilters.Models.Repos
{
    public interface IBookFilterRepository
    {
        List<SearchFilter> GetSearchFilters(int[] author, int[] protagonist, int[] country);
    }


    public class BookFilterRepository : IBookFilterRepository, IDisposable
    {
        private BookContext _db = new BookContext();
        private IBookRepository _bookRepo;
        
        public BookFilterRepository(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        
        public List<SearchFilter> GetSearchFilters(
            int[] author,
            int[] protagonist,
            int[] country
        )
        {
            // author filter
            var books = _bookRepo.Search(null, protagonist, country);
            var authorGroups = books.Results.SelectMany(b => b.Authors)
                .GroupBy(b => b.AuthorId)
                .ToList();

            var temp = 0;
            var authorFilter = new SearchFilter
            {
                FilterName = "Author",
                FilterQsKey = "author",
                FilterTerms = authorGroups.Select(g => new FilterTerm
                {
                    Id = g.Key.ToString(),
                    Name = g.First().FullName,
                    Count = g.Count(),
                    NestedFilters = new List<SearchFilter>
                    {
                        new SearchFilter{FilterName="Test", FilterQsKey="test", FilterTerms=new List<FilterTerm>{
                            new FilterTerm{Id=$"test{temp++}", Name="All", Count=5, IsAllOption=true},
                            new FilterTerm{Id=$"test{temp++}", Name="name1", Count=3},
                            new FilterTerm{Id=$"test{temp++}", Name="name2",Count=2}
                        } }
                    }


                }).ToList()
            };

            // protagonist filter
            books = _bookRepo.Search(author, null, country);
            var l = books.Results.ToList();
            var protagonistGroups = books.Results.SelectMany(b => b.Protagonists)
                .GroupBy(p => p.ProtagonistId)
                .ToList();

            var protagonistFilter = new SearchFilter
            {
                FilterName = "Protagonist",
                FilterQsKey = "protagonist",
                FilterTerms = protagonistGroups.Select(g => new FilterTerm
                {
                    Id = g.Key.ToString(),
                    Name = g.First().FullName,
                    Count = g.Count()
                }).ToList()
            };

            // country filter
            books = _bookRepo.Search(author, protagonist, null);
            var countryGroups = books.Results.SelectMany(b => b.Authors.Select(a => a.Country))
                .GroupBy(a => a.CountryId)
                .ToList();

            var countryFilter = new SearchFilter
            {
                FilterName = "Country",
                FilterQsKey = "country",
                FilterTerms = countryGroups.Select(g => new FilterTerm
                {
                    Id = g.Key.ToString(),
                    Name = g.First().Name,
                    Count = g.Count()

                }).ToList()
            };

            var list = new List<SearchFilter>();
            list.Add(authorFilter);
            list.Add(protagonistFilter);
            list.Add(countryFilter);

            return list;
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


    class GroupDto<T>
    {
        public Book Book { get; set; }
        public T Entity { get; set; }
        public int EntityId { get; set; }
    }
}