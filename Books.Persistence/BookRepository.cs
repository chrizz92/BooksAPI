using System.Collections.Generic;
using System.Linq;
using Books.Core.Contracts;
using Books.Core.Entities;

namespace Books.Persistence
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Books.Count();
        }

        public void Delete(Book book)
        {
            _dbContext.Books.Remove(book);
        }

        public Book[] GetBooks(int? selectedPub_id)
        {
            if (selectedPub_id != null && selectedPub_id != 0)
                return _dbContext.Books.Where(b => b.Publisher_Id == selectedPub_id).OrderBy(b => b.Title).ToArray();
            else
                return _dbContext.Books.ToArray();
        }

        public IEnumerable<Book> GetByFilter(string filter)
        {
            return _dbContext.Books.Where(b => b.Title.ToLower().Contains(filter.ToLower())).ToList();
        }

        public Book GetById(int id)
        {
            return _dbContext.Books.Single(b => b.Id == id);
        }

        public void Insert(Book book)
        {
            _dbContext.Books.Add(book);
        }


    }
}