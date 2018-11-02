using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models.Filters;
using WebAppWithSearchFilters.Models.ModelBinders;
using WebAppWithSearchFilters.Models.Repos;

namespace WebAppWithSearchFilters.Controllers
{
    public class BookFilterController : ApiController
    {
        private IBookFilterRepository _filterStore;

        public BookFilterController(IBookFilterRepository filterStore)
        {
            _filterStore = filterStore;
        }

        // GET api/values
        public List<SearchFilter> Get(
            [CsvToArrayModelBinder]int[] author = null,
            [CsvToArrayModelBinder]int[] protagonist = null,
            [CsvToArrayModelBinder]int[] country = null
        )
        {
            var searchFilters = _filterStore.GetSearchFilters(author, protagonist, country);

            return searchFilters;
        }

        
    }
}
