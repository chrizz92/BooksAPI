using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.ViewModels
{
    public class BooksListByPublisherViewModel
    {
        public Book[] Books { get; set; }
        public SelectList PublisherList { get; set; }

        public int? SelectedPubId { get; set; }

        public void LoadData(IUnitOfWork uow)
        {

            Books = uow.BookRepository.GetBooks(SelectedPubId);

            List<Publisher> pupList = uow.PublisherRepository.GetAll().ToList<Publisher>();
            pupList.Insert(0, new Publisher()
            {
                Id = 0,
                Name = "< Alle anzeigen >"
            });
            PublisherList = new SelectList(pupList, "Id", "Name");
        }
    }
}
