using DecentReadsApi.Entities;
using DecentReadsApi.Models;
using DecentReadsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DecentReadsApi.Controllers
{
    [Route("api/favoritebooks")]
    [ApiController]
    public class FavoriteBooksController : Controller
    {
        private readonly IFavoriteBookService bookService;

        public FavoriteBooksController(GoodreadsDbContext dbContext, IFavoriteBookService bookService)
        {
            this.bookService = bookService;
        }

        [Authorize]
        [HttpPost("{bookId}")]
        public ActionResult AddToFavorites([FromRoute] int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            bookService.AddToFavorites(bookId, userId);
            return Ok();
        }


        

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FavoriteBookDto>>> GetFavoriteBooksByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await bookService.GetFavoriteBooksByUserId(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{bookId}")]
        public ActionResult DeleteFromFavorites([FromRoute] int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            bookService.DeleteFromFavorites(bookId, userId);
            return Ok();
        }

    }
}
