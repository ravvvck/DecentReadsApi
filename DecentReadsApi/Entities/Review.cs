using System.ComponentModel.DataAnnotations;

namespace DecentReadsApi.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [RegularExpression(@"1|2|3|4|5")]
        public int Rating { get; set; }
        [MaxLength(250)]
        public string? ReviewContent { get; set; }
        public string  MadeBy { get; set; } = string.Empty;
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
