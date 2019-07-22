using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithSearchFilters.Models
{
    class DbContextInitializer : MigrateDatabaseToLatestVersion<BookContext, MigrateDbConfiguration> //DropCreateDatabaseIfModelChanges<TestRunsDbContext>
    {
    }
}
