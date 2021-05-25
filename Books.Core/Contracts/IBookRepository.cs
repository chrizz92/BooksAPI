using System;
using System.Collections.Generic;
using System.Text;
using Books.Core.Entities;

namespace Books.Core.Contracts
{
    public interface IBookRepository
    {

        int Count();
        Book[] GetBooks(int? selectedPub_id);
        Book GetById(int id);
        void Delete(Book book);
        void Insert(Book book);
        IEnumerable<Book> GetByFilter(string filter);
    }
}
