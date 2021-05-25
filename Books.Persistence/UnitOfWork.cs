using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Utils;
using Books.Core.Contracts;
using Books.Core.Entities;

namespace Books.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        const string FILENAME = "books.csv";

        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private bool _disposed;


        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            PublisherRepository = new PublisherRepository(_dbContext);
            BookRepository = new BookRepository(_dbContext);
        }

        public IPublisherRepository PublisherRepository { get; }

        public IBookRepository BookRepository { get; }


        /// <summary>
        ///     Repository-übergreifendes Speichern der Änderungen
        /// </summary>
        public void Save()
        {
            var entities = _dbContext.ChangeTracker.Entries()
               .Where(entity => entity.State == EntityState.Added
                                || entity.State == EntityState.Modified)
               .Select(e => e.Entity).ToList();
            foreach (var entity in entities)
            {
                ValidateEntity(entity);
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Validierungen auf DbContext-Ebene
        /// </summary>
        /// <param name="entity"></param>
        private void ValidateEntity(object entity)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteDatabase()
        {
            _dbContext.Database.EnsureDeleted();
        }

        public void MigrateDatabase()
        {
            try
            {
                _dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FillDb()
        {
            this.DeleteDatabase();
            this.MigrateDatabase();
            string[][] csvFile = MyFile.ReadStringMatrixFromCsv(FILENAME,true);

            var publishers = csvFile.GroupBy(line => line[2]).Select(grp =>
                new Publisher()
                {
                    Name = grp.Key
                }).ToList();

            var books = csvFile.GroupBy(line => line[3]).Select(grp =>
                new Book()
                {
                    Title = grp.First()[1],
                    Authors = grp.First()[0],
                    Publisher = publishers.First(p => p.Name == grp.First()[2]),
                    Isbn = grp.First()[3]
                }).ToList();

            _dbContext.Books.AddRange(books);
            _dbContext.SaveChanges();

        }
    }


}
