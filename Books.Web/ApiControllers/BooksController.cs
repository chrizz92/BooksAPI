using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Web.ApiControllers
{
    [Route("api/[controller]",Name ="books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public BooksController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/<BooksController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_uow.BookRepository.GetBooks(null));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_uow.BookRepository.GetById(id));
        }

        [Route("getBookByName")]
        [HttpGet]
        public IActionResult Get(string filter)
        {
            return Ok(_uow.BookRepository.GetByFilter(filter));
        }
    }
}
