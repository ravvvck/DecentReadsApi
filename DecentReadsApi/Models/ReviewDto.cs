namespace DecentReadsApi.Models
{
    public class ReviewDto
    {
        
            public int Id { get; set; }
            public int Rating { get; set; }
            public string? ReviewContent { get; set; }
            public string MadeBy { get; set; } = string.Empty;
            public int BookId { get; set; }
            public string BookTitle { get; set; }
            public string BookAuthor { get; set; }
            public int BookAuthorId { get; set; }
            public DateTime Created { get; set; } = DateTime.Now;

        
    }
}
