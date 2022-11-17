using AutoMapper;
using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DecentReadsApi.Services
{
    public interface IReviewService
    {
        int Create(CreateReviewDto newReview, string username);
        Task<List<ReviewDto>> GetAll();
        Review GetById(int id);
        IEnumerable<Review> GetByBookId(int id);
        void Update(CreateReviewDto dto, string user);
    }

    public class ReviewService : IReviewService
    {
        private readonly DecentReadsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<BookService> logger;

        public ReviewService(DecentReadsDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }


        public async Task<List<ReviewDto>> GetAll()
        {
            var reviews = await dbContext.Reviews.Include(b => b.Book).Include(b => b.Book.Author).ToListAsync();
            var reviewsDtos = mapper.Map<List<ReviewDto>>(reviews);
            return reviewsDtos;
        }

        public Review GetById(int id)
        {
            var review = dbContext.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null) throw new NotFoundException("Review not found");

            return review;
        }

        public int Create(CreateReviewDto newReviewDto, string username)
        {
            var validation = dbContext.Reviews.FirstOrDefault(r => r.BookId == newReviewDto.BookId & r.MadeBy == username);

            if (validation != null)
            {
                throw new BadRequestException("User has already rated this book");

            }
            var book = dbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == newReviewDto.BookId);
            var newReview = mapper.Map<Review>(newReviewDto);
            newReview.Book = book;
            newReview.MadeBy = username;

            dbContext.Reviews.Add(newReview);
            dbContext.SaveChanges();
            return newReview.Id;
        }
        public IEnumerable<Review> GetByBookId(int id)
        {
            var reviews = dbContext.Reviews.Where(r => r.BookId == id);
            if (reviews == null) throw new NotFoundException("No reviews");

            return reviews;
        }




        public void Delete(int id)
        {
            logger.LogError($"Reciew with id: {id} DELETE action invoked");
            var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) throw new NotFoundException("Review not found");

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();

        }

        public void Update(CreateReviewDto dto, string user)
        {
            var existingReview = dbContext.Reviews.FirstOrDefault(r => r.BookId == dto.BookId & r.MadeBy == user);
            if (existingReview == null) throw new BadRequestException("");

            var updatedReview = mapper.Map<Review>(dto);
            updatedReview.Id = existingReview.Id;
            dbContext.Entry(existingReview).CurrentValues.SetValues(updatedReview);
            dbContext.SaveChanges();
        }

    }
}
