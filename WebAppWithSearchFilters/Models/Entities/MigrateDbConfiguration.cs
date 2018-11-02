using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithSearchFilters.Models
{
    public class MigrateDbConfiguration : DbMigrationsConfiguration<BookContext>
    {
        public MigrateDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookContext context)
        {
            if (context.Countries.Any())
                return;

            var russia = new Country
            {
                Name = "Russia",
            };
            context.Countries.Add(russia);
            
            var england = new Country
            {
                Name = "England",
            };
            context.Countries.Add(england);

            var newZeland = new Country
            {
                Name = "New Zeland",
            };
            context.Countries.Add(newZeland);

            var dostoyevsky = new Author
            {
                FirstName = "Fyodor",
                LastName = "Dostoyevsky",
                Country = russia
            };
            context.Authors.Add(dostoyevsky);

            var tolstoy = new Author
            {
                FirstName = "Leo",
                LastName = "Tolstoy",
                Country = russia
            };
            context.Authors.Add(tolstoy);
            
            var grunwald = new Author
            {
                FirstName = "Constantin",
                LastName = " de Grunwald",
                Country = england
            };
            context.Authors.Add(grunwald);

            var garvin = new Author
            {
                FirstName = "Viola",
                LastName = "Garvin",
                Country = england
            };
            context.Authors.Add(garvin);

            var grey = new Author
            {
                FirstName = "Ian",
                LastName = "Grey",
                Country = newZeland
            };
            context.Authors.Add(garvin);

            var peterTheGreat = new Protagonist
            {
                FirstName = "Peter",
                LastName = "the Great"
            };

            //  This method will be called after migrating to the latest version.
            context.Books.AddOrUpdate(new[]
            {
                new Book
                {
                    Title = "The Brothers Karamazov",
                    Authors = { dostoyevsky },
                    Protagonists = new List<Protagonist>
                    {
                        new Protagonist{ FirstName = "Alexei", LastName = "Karamazov" },
                        new Protagonist{ FirstName = "Dmitri", LastName = "Karamazov" },
                        new Protagonist{ FirstName = "Ivan", LastName = "Karamazov" },
                        new Protagonist{ FirstName = "Fyodor", LastName = "Karamazov" },

                    }
                },
                new Book
                {
                    Title = "Crime And Punishment",
                    Authors = { dostoyevsky },
                    Protagonists = new List<Protagonist>
                    {
                        new Protagonist{ FirstName = "Rodion", LastName = "Raskolnikov" },
                        new Protagonist{ FirstName = "Sofya", LastName = "Marmeladov" },
                        new Protagonist{ FirstName = "Avdotya", LastName = "Raskolnikov" }

                    }
                },
                new Book
                {
                    Title = "War And Peace",
                    Authors = { tolstoy },
                    Protagonists = new List<Protagonist>
                    {
                        new Protagonist{ FirstName = "Anna", LastName = "Pavlovna" },
                        new Protagonist{ FirstName = "Pierre", LastName = "Bezukhov" }

                    }
                },
                new Book
                {
                    Title = "Peter the Great",
                    Authors = { grunwald, garvin },
                    Protagonists = new List<Protagonist>
                    {
                        peterTheGreat
                    }
                },
                new Book
                {
                    Title = "Peter the Great: Emperor of All Russia",
                    Authors = { grey },
                    Protagonists = new List<Protagonist>
                    {
                        peterTheGreat
                    }
                },
                new Book
                {
                    Title = "Tsar Nicholas I: Life of An Absolute Monarch",
                    Authors = { grunwald },
                    Protagonists = new List<Protagonist>
                    {
                        new Protagonist{ FirstName = "Nicholas", LastName = "Romanov" }
                    }
                }
            });
        }
    }
}
