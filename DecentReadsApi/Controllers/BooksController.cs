using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using DecentReadsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace DecentReadsApi.Controllers
{
    [Route("api/books")]
    [ApiController]

    public class BooksController : Controller
    {
        private readonly GoodreadsDbContext dbContext;
        private readonly IBookService bookService;

        public BooksController(GoodreadsDbContext dbContext, IBookService bookService)
        {
            this.dbContext = dbContext;
            this.bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var books = await bookService.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var book = bookService.GetById(id);
            return Ok(book);
        }

        [HttpGet("range")]
        public ActionResult GetByIdRange([FromQuery] List<int> ids)
        {
            var book = bookService.GetByIdRange(ids);
            return Ok(book);
        }

        [Authorize(Roles = "Admin, Librarian")]
        [HttpPost]
        public ActionResult CreateBook([FromBody] CreateBookDto dto)
        {
            var id = bookService.Create(dto);
            return Created("/api/books/{id}", null);
        }



        [Authorize(Roles = "Admin, Librarian")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            bookService.Delete(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin, Librarian")]
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateBookDto dto)
        {
            bookService.Update(id,dto);
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult Search([FromQuery]string name)
        {
            if (name == null)
            {
                throw new NotFoundException("Book not found");
            }
            try
                
            {

                var result = dbContext.Books.Include(b => b.Author)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()));

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return (NotFound());
                }

                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

       



    }
}
