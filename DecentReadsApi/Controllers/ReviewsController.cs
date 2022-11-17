using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using DecentReadsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DecentReadsApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]

    public class ReviewsController : Controller
    {
        private readonly GoodreadsDbContext dbContext;
        private readonly IReviewService reviewService;

        public ReviewsController(GoodreadsDbContext dbContext, IReviewService reviewService)
        {
            this.dbContext = dbContext;
            this.reviewService = reviewService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAll()
        {
            var reviews = await reviewService.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var review = reviewService.GetById(id);
            return Ok(review);
        }

        [HttpGet("book/{id}")]
        public ActionResult GetByBookId([FromRoute] int id)
        {
            var review = reviewService.GetByBookId(id);
            return Ok(review);
        }

        [Authorize(Roles = "User, Admin, Librarian")]
        [HttpPost]
        public ActionResult CreateReview([FromBody] CreateReviewDto newReviewDto)
        {
            string username = User.FindFirst(ClaimTypes.Name)?.Value;

            var id = reviewService.Create(newReviewDto, username);
            return Created($"{id}", null);
        }

        [Authorize(Roles = "User, Admin, Librarian")]
        [HttpPut()]
        public ActionResult Update( [FromBody] CreateReviewDto dto)
        {
            string user = User.FindFirst(ClaimTypes.Name)?.Value;
            reviewService.Update( dto, user);
            return Ok();
        }
    }
}
