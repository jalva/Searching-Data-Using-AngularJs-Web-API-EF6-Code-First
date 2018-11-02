using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models.BookData;
using WebAppWithSearchFilters.Models.Filters;
using WebAppWithSearchFilters.Models.ModelBinders;
using WebAppWithSearchFilters.Models.Repos;
using WebAppWithSearchFilters.Models.Search;

namespace WebAppWithSearchFilters.Controllers
{
    public class BookSchemaController : ApiController
    {

        // GET api/values
        public List<ItemColumn> Get()
        {
            return new List<ItemColumn>
            {
                new ItemColumn
                {
                    Title="Id", Field="bookId", Visible=true, Sortable=true, width="20%", OrderBy= BookOrderBy.BookId.ToString(), Index=1
                },
                new ItemColumn
                {
                    Title="Title", Field="title", Visible=true, Sortable=true, OrderBy= BookOrderBy.BookTitle.ToString(), Index=2
                }
            };
        }

        
    }
}
