using DecentReadsApi.Entities;

namespace DecentReadsApi.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}
