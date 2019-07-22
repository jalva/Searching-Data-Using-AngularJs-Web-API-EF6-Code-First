using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models2.Repositories;

namespace WebAppWithSearchFilters.Models2.UnitOfWork
{
    public interface IMyUnitOfWork
    {
        DbContextTransaction BeginTransaction();
        string ConnString { get; }
        void SaveChanges();
        GenericRepository<Book> BookRepo { get; }
    }
}
