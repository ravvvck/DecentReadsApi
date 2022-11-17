using AutoMapper;
using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;

namespace DecentReadsApi.Services
{
    public interface IAuthorService
    {
        int Create(AuthorDto dto);
        void Delete(int id);
        IEnumerable<AuthorDto> GetAll();
        AuthorDto GetById(int id);
        void Update(int id, AuthorDto dto);
    }

    public class AuthorService : IAuthorService
    {
        private readonly GoodreadsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<BookService> logger;

        public AuthorService(GoodreadsDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            var authors = dbContext.Authors.ToList();
            var authorsDtos = mapper.Map<List<AuthorDto>>(authors);
            return authorsDtos;
        }

        public AuthorDto GetById(int id)
        {
            var author = dbContext.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null) throw new NotFoundException("Author not found");

            var result = mapper.Map<AuthorDto>(author);
            return result;
        }

        public int Create(AuthorDto dto)
        {
            var author = dbContext.Authors.FirstOrDefault(a => a.FirstName == dto.FirstName & a.LastName == dto.LastName);
            if (author != null)
            {
                throw new AlreadyExistException("Author already exist");

            }
            var authorDto = mapper.Map<Author>(dto);
            dbContext.Authors.Add(authorDto);
            dbContext.SaveChanges();
            return authorDto.Id;
        }
        public void Delete(int id)
        {
            logger.LogError($"Author with id: {id} DELETE action invoked");
            var author = dbContext.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null) throw new NotFoundException("Author not found");

            dbContext.Authors.Remove(author);
            dbContext.SaveChanges();

        }
        public void Update(int id, AuthorDto dto)
        {
            var existingAuthor = dbContext.Authors.FirstOrDefault(b => b.Id == id);
            if (existingAuthor == null) throw new NotFoundException("Author not found");

            var newBook = mapper.Map<Author>(dto);
            newBook.Id = existingAuthor.Id;
            dbContext.Entry(existingAuthor).CurrentValues.SetValues(newBook);
            dbContext.SaveChanges();

        }
    }
}
