using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Books.Core.Contracts;
using Books.Core.Entities;
using Books.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Controllers
{
    public class BooksController : Controller
    {
        IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult ListByPublisher(BooksListByPublisherViewModel model)
        {
            model.LoadData(_unitOfWork);
            return View(model);
        }

        public IActionResult DeleteBook(int id)
        {
            Book book = _unitOfWork.BookRepository.GetById(id);
            if (book == null)
                return NotFound();
            _unitOfWork.BookRepository.Delete(book);
            _unitOfWork.Save();
            return RedirectToAction(nameof(ListByPublisher), new { SelectedPubId = book.Publisher_Id });
        }

        public IActionResult Create(int publisherId)
        {
            BooksCreateViewModel model = new BooksCreateViewModel();
            model.LoadSelectList(_unitOfWork);
            model.Book.Publisher_Id = publisherId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(BooksCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BookRepository.Insert(model.Book);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(ListByPublisher), new { SelectedPub_id = model.Book.Publisher_Id });
                }
                catch (ValidationException ex)
                {
                    var res = ex.ValidationResult;
                    ModelState.AddModelError(nameof(model.Book) + "." + res.MemberNames.First(), res.ErrorMessage);
                }
            }

            //An dieser Stelle ist der ModelState mit Sicherheit false
            
            model.LoadSelectList(_unitOfWork);
            return View(model);
            

            
        }
    }
}