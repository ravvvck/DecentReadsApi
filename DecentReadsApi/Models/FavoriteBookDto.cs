namespace DecentReadsApi.Models
{
    public class FavoriteBookDto
    {
        public int Id { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int BookAuthorId { get; set; }
        public string BookAuthor { get; set; }
    }
}
