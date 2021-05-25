using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Web.ApiControllers
{
    [Route("api/[controller]",Name ="publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public PublishersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/<PublishersController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_uow.PublisherRepository.GetAll());
        }

        // GET api/<PublishersController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Publisher publisher = _uow.PublisherRepository.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/<PublishersController>
        [HttpPost]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _uow.PublisherRepository.Insert(publisher);                
                try
                {
                    _uow.Save();
                }
                catch(DbUpdateException e)
                {

                }
                return new CreatedAtActionResult(nameof(GetById), nameof(PublishersController), new { id = publisher.Id }, publisher);
            }
            return BadRequest();
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid || id != publisher.Id)
            {
                return BadRequest();
            }
            _uow.PublisherRepository.UpdatePublisher(publisher);
            try
            {
                _uow.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_uow.PublisherRepository.GetPublisherById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw new DbUpdateException();
                }
            }

            return Ok(publisher);
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Publisher publisher = _uow.PublisherRepository.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            _uow.PublisherRepository.DeletePublisher(publisher);
            _uow.Save();
            return Ok(publisher);
        }
    }
}
