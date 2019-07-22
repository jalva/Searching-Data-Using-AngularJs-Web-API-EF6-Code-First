using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAppWithSearchFilters.Models;
using WebAppWithSearchFilters.Models2.Repositories;

namespace WebAppWithSearchFilters.Models2.UnitOfWork
{
    public class MyEFUnitOfWork : IMyUnitOfWork, IDisposable
    {
        private BookContext _dbContext;

        private string _connString;
        public string ConnString
        {
            get
            {
                //return _dbContext.Database.Connection.ConnectionString;
                return _connString;
            }
        }

        private GenericRepository<Book> _bookRepo;
        public GenericRepository<Book> BookRepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new GenericRepository<Book>(_dbContext);
                return _bookRepo;
            }
        }

        public MyEFUnitOfWork()
        {
            _connString = ""; 
            var dbConnection = new SqlConnection(ConnString);
            _dbContext = new BookContext(dbConnection, true);
        }

        public DbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        } 
    }
}