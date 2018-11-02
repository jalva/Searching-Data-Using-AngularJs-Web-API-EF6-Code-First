using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Dtos
{

    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public List<ProtagonistDto> Protagonists { get; set; }

        public BookDto() { }

        public BookDto(Book b)
        {
            BookId = b.BookId;
            Title = b.Title;
            Authors = b.Authors.Select(a => new AuthorDto(a)).ToList();
            Protagonists = b.Protagonists.Select(p => new ProtagonistDto(p)).ToList();
        }
    }

}