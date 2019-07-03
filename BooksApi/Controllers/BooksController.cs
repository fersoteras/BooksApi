using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    // Following service  uses BookService to  perform CRUD operations.
    // Contain action methods that support  GET POST PUT and DELETE HTTP requests.
    // Calls CreatedAtRoute in the create  action method to return an HTTP 201. 
    // (201 ::The request has been fulfilled and resulted in a new resource being created. )
    // CreatedAtRoute adds a Location header to the response. The location header specifies
    // the  URI of the newly created book.

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // filed for ref to service.
        private readonly BookService _bookService;

        //Constructor initializing service.
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // get verb 
        [HttpGet]
        public ActionResult<List<Book>> Get() => _bookService.Get();


        
        
        //[HttpGet("{id:lenght(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // Post verb case

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            // service accesing aspect.
            _bookService.Create(book);

            // http response with value returned aspect.
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            //
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            //service access aspect.
            _bookService.Update(id, bookIn);

            // http response aspect
            return NoContent();
        }
        //delete verb.

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }



    }


}

    

