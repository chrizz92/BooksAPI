using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Books.Core.Contracts;
using Books.Core.DTOs;
using Books.Core.Entities;

namespace Books.Persistence
{
    internal class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PublisherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Publishers.Count();
        }

        public void DeletePublisher(Publisher publisher)
        {
            _dbContext.Publishers.Remove(publisher);
        }

        public Publisher[] GetAll()
        {
            return _dbContext.Publishers.OrderBy(p => p.Name).ToArray();
        }

        public Publisher GetPublisherById(int id)
        {
            return _dbContext.Publishers.Where(p => p.Id == id).FirstOrDefault();
        }

        public PublisherWithBookCnt[] GetPublisherWithBookCnt(int? filterFrom = null, int? filterTo = null)
        {
            return _dbContext.Publishers.Select(p =>
              new PublisherWithBookCnt()
              {
                  Id = p.Id,
                  Name = p.Name,
                  BookCnt = p.Books.Count()
              }).Where(p => p.BookCnt >= (filterFrom ?? int.MinValue) && (p.BookCnt <= (filterTo ?? int.MaxValue))).OrderBy(p => p.Name).ToArray();

        }

        public Publisher GetPublisherWithMostBooks()
        {
            return _dbContext.Publishers.OrderByDescending(a => a.Books.Count()).FirstOrDefault();
        }

        public void Insert(Publisher publisher)
        {
            _dbContext.Publishers.Add(publisher);
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _dbContext.Publishers.Update(publisher);
        }
    }
}