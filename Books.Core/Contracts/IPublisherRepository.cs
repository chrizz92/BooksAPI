using System;
using System.Collections.Generic;
using System.Text;
using Books.Core.DTOs;
using Books.Core.Entities;

namespace Books.Core.Contracts
{
    public interface IPublisherRepository
    {

        int Count();
        PublisherWithBookCnt[] GetPublisherWithBookCnt(int? filterFrom = null, int? filterTo = null);
        Publisher GetPublisherWithMostBooks();
        Publisher[] GetAll();
        Publisher GetPublisherById(int id);
        void Insert(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
    }
}
