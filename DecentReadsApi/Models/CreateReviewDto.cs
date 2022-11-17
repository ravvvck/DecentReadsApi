using System.ComponentModel.DataAnnotations;

namespace DecentReadsApi.Models
{
    public class CreateReviewDto
    {
        [RegularExpression(@"1|2|3|4|5")]
        public int Rating { get; set; }
        [MaxLength(250)]
        public string? ReviewContent { get; set; }
        public int BookId { get; set; }
    }
}
