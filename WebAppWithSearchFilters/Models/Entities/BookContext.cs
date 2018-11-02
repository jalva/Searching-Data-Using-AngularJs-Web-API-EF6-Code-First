
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebAppWithSearchFilters.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Protagonist> Protagonists { get; set; }
        public DbSet<Country> Countries { get; set; }
        

        public BookContext() : this("name=Books")
        {
        }

        public BookContext(string connString) : base(connString)
        {
            Database.SetInitializer(new TestRunsDbContextDbInitializer());
        }
        
    }

    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Index("IX_Unique_Book", IsUnique = true)]
        public string Title { get; set; }
        
        public List<Author> Authors { get; set; }
        public List<Protagonist> Protagonists { get; set; }

        public Book()
        {
            Authors = new List<Author>();
            Protagonists = new List<Protagonist>();
        }
    }

    public class Protagonist
    {
        public int ProtagonistId { get; set; }
        [Index("IX_Unique_Protagonist", IsUnique = true, Order = 0)]
        public string FirstName { get; set; }
        [Index("IX_Unique_Protagonist", IsUnique = true, Order = 1)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public Gender Gender { get; set; }

        public List<Book> Books { get; set; }

        public Protagonist()
        {
            Books = new List<Book>();
        }
    }
    
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Index("IX_Unique_Author", IsUnique = true, Order = 0)]
        public string FirstName { get; set; }
        [Index("IX_Unique_Author", IsUnique = true, Order = 1)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Book> Books { get; set; }
        public Gender Gender { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }

    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Index("IX_Unique_Country", IsUnique = true)]
        public string Name { get; set; }
        public List<Author> Authors { get; set; }

        public Country()
        {
            Authors = new List<Author>();
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
