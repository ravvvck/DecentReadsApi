using AutoMapper;
using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DecentReadsApi.Services
{
    public interface IFavoriteBookService
    {
        void AddToFavorites(int bookId, int userId);
        void DeleteFromFavorites(int bookId, int userId);
        Task<List<FavoriteBookDto>> GetFavoriteBooksByUserId(int userId);
    }

    public class FavoriteBookService : IFavoriteBookService
    {
        private readonly DecentReadsDbContext dbContext;

        private readonly IMapper mapper;

        public FavoriteBookService(DecentReadsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public void AddToFavorites(int bookId, int userId)
        {
            var existingBook = dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            var validation = dbContext.FavoriteBooks.FirstOrDefault(r => r.BookId == bookId & r.UserId == userId);
            if (existingBook == null)
            {
                throw new NotFoundException("Book does not exist");
            }
            if (validation != null)
            {
                throw new BadRequestException("User has already added this book to favorites");

            }
            var newFavBook = new FavoriteBook()
            {
                BookId = bookId,
                UserId = userId,
                Book = existingBook
            };

            dbContext.FavoriteBooks.Add(newFavBook);
            dbContext.SaveChanges();
        }
        public async Task<List<FavoriteBookDto>> GetFavoriteBooksByUserId(int userId)
        {
            var favorites = await dbContext.FavoriteBooks.Include(b => b.Book).Include(b => b.Book.Author).Where(b => b.UserId == userId).ToListAsync();
            var favoriteDtos = mapper.Map<List<FavoriteBookDto>>(favorites);
            return favoriteDtos;
        }


        public void DeleteFromFavorites(int bookId, int userId)
        {
            var favBook = dbContext.FavoriteBooks.FirstOrDefault(b => b.BookId == bookId & b.UserId == userId);
            if (favBook != null)
            {
                dbContext.FavoriteBooks.Remove(favBook);
                dbContext.SaveChanges();
            }
        }
    }
}
