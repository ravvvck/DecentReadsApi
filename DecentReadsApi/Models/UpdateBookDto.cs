using System.ComponentModel.DataAnnotations;

namespace DecentReadsApi.Models
{
    public class UpdateBookDto
    {
        [Required]
        public string Name { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}
