using AutoMapper;
using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DecentReadsApi.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAll();
        BookDto GetById(int id);
        IEnumerable<BookDto> GetByIdRange(List<int> ids);
        int Create(CreateBookDto dto);
        void Delete(int id);
        void Update(int id, UpdateBookDto dto);
        
   }

    public class BookService : IBookService
    {
        private readonly GoodreadsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<BookService> logger;

        public BookService(GoodreadsDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<List<BookDto>> GetAll()
        {
            var books = await dbContext.Books.Include(b => b.Author).ToListAsync();
            var booksDtos = mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }

        public BookDto GetById(int id)
        {
            var book = dbContext.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
            if (book == null) throw new NotFoundException("Book not found");

            var result = mapper.Map<BookDto>(book);
            return result;
        }
        public IEnumerable<BookDto> GetByIdRange(List<int> ids)
        {
            var books = dbContext.Books.Include(b => b.Author).Where(b => ids.Contains(b.Id));
            var booksDtos = mapper.Map<List<BookDto>>(books);
            return booksDtos;
        }

        public int Create(CreateBookDto dto)
        {
            var author = dbContext.Authors.FirstOrDefault(a => a.FirstName == dto.AuthorFirstName & a.LastName == dto.AuthorLastName);
            var book = mapper.Map<Book>(dto);
            if (author != null)
            {
                book.Author = author;

            }
            
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return book.Id;
        }
        public void Delete(int id)
        {
            logger.LogError($"Book with id: {id} DELETE action invoked");
            var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) throw new NotFoundException("Book not found");

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
            
        }
        public void Update(int id, UpdateBookDto dto)
        {
            var existingBook = dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null) throw new NotFoundException("Book not found");

            var newBook = mapper.Map<Book>(dto);
            newBook.Id = existingBook.Id;
            dbContext.Entry(existingBook).CurrentValues.SetValues(newBook);
            dbContext.SaveChanges();
        }

       
    }
}
