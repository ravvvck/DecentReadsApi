using DecentReadsApi.Entities;
using DecentReadsApi.Models;
using DecentReadsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DecentReadsApi.Controllers
{
    [Route("api/authors")]
    [ApiController]

    public class AuthorsController : Controller
    {
        private readonly DecentReadsDbContext dbContext;
        private readonly IAuthorService authorService;

        public AuthorsController(DecentReadsDbContext dbContext, IAuthorService authorService)
        {
            this.dbContext = dbContext;
            this.authorService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetAll()
        {
            var books = authorService.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var book = authorService.GetById(id);
            return Ok(book);
        }

        [HttpPost]
        public ActionResult CreateBook([FromBody] AuthorDto dto)
        {
            var id = authorService.Create(dto);
            return Created($"/api/authors/{id}", null);
        }




        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            authorService.Delete(id);
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] AuthorDto dto)
        {
            authorService.Update(id, dto);
            return Ok();
        }

    }
}
