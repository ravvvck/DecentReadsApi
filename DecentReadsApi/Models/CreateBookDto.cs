using System.ComponentModel.DataAnnotations;

namespace DecentReadsApi.Models
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorFirstName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime PublishedDate { get; set; }
        public int NumberOfPages { get; set; }
    }
}
