using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Dtos
{

    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public AuthorDto() { }

        public AuthorDto(Author a)
        {
            AuthorId = a.AuthorId;
            FirstName = a.FirstName;
            LastName = a.LastName;
        }
    }

}