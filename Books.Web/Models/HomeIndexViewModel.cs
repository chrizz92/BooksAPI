using Books.Core.Contracts;
using Books.Core.DTOs;
using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        [Display(Name = "Von")]
        public int? FilterFrom { get; set; }
        [Display(Name = "Bis")]
        public int? FilterTo { get; set; }

        public PublisherWithBookCnt[] Publishers;

        public Publisher TopPublisher { get; set; }

        public void LoadData(IUnitOfWork uow)
        {

            Publishers = uow.PublisherRepository.GetPublisherWithBookCnt(FilterFrom,FilterTo);
                
                
            TopPublisher = uow.PublisherRepository.GetPublisherWithMostBooks();
        }
    }
}
