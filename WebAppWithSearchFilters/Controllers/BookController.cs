using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models.BookData;
using WebAppWithSearchFilters.Models.Dtos;
using WebAppWithSearchFilters.Models.Filters;
using WebAppWithSearchFilters.Models.ModelBinders;

namespace WebAppWithSearchFilters.Controllers
{
    public class BookController : ApiController
    {
        private IBookRepository _bookStore;

        public BookController(IBookRepository bookStore)
        {
            _bookStore = bookStore;
        }

        // GET api/values
        public SearchResponseDto<BookDto> Get(
            [CsvToArrayModelBinder]int[] author = null,
            [CsvToArrayModelBinder]int[] protagonist = null,
            [CsvToArrayModelBinder]int[] country = null,
            BookOrderBy? orderBy = null,
            int? page = null,
            int? count = null,
            bool? orderDesc = null
        )
        {
            var query = _bookStore.Search(author, protagonist, country, page, count, orderBy, orderDesc);

            var bookDtos = query.Results.ToList().Select(b => new BookDto(b)).ToList();

            var dtos = new SearchResponseDto<BookDto>
            {
                Results = bookDtos,
                TotalCount = query.TotalCount
            };

            return dtos;
        }


        public IHttpActionResult Get(int id)
        {
            var book = _bookStore.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new BookDto(book));
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        
    }
    
}
