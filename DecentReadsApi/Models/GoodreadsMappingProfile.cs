using AutoMapper;
using DecentReadsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DecentReadsApi.Models
{
    public class GoodreadsMappingProfile : Profile
    {
        private readonly GoodreadsDbContext dbContext;

        public GoodreadsMappingProfile(GoodreadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckIfAuthorExist(string firstname, string lastname)
        {
            var author = dbContext.Authors.Any(a => a.FirstName == firstname & a.LastName == lastname);
            if (author == null) { return false; }
            return true;
        }
        public Book GetById(int id)
        {
            var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
            return book;
        }

        public GoodreadsMappingProfile()
        {
            CreateMap<Book, BookDto>()
           .ForMember(m => m.AuthorFirstName, c => c.MapFrom(s => s.Author.FirstName))
           .ForMember(m => m.AuthorLastName, c => c.MapFrom(s => s.Author.LastName));

            CreateMap<Review, ReviewDto>()
           .ForMember(m => m.BookTitle, c => c.MapFrom(s => s.Book.Name))
           .ForMember(m => m.BookAuthor, c => c.MapFrom(s => s.Book.Author.FirstName + s.Book.Author.LastName))
           .ForMember(m => m.BookAuthorId, c => c.MapFrom(s => s.Book.Author.Id));

           CreateMap<FavoriteBook, FavoriteBookDto>()
          .ForMember(m => m.BookTitle, c => c.MapFrom(s => s.Book.Name))
          .ForMember(m => m.BookAuthor, c => c.MapFrom(s => s.Book.Author.FirstName + s.Book.Author.LastName))
          .ForMember(m => m.BookAuthorId, c => c.MapFrom(s => s.Book.Author.Id));


            CreateMap<CreateBookDto,Book>()
                .ForMember(r => r.Author,
                c => c.MapFrom(dto => new Author() { FirstName = dto.AuthorFirstName, LastName = dto.AuthorLastName }));

            

            CreateMap<UpdateBookDto, Book>()
                .ForMember(r => r.Author,
                c => c.MapFrom(dto => new Author() { FirstName = dto.AuthorFirstName, LastName = dto.AuthorLastName }));
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<CreateReviewDto, Review>();
                

        }
    }
}
