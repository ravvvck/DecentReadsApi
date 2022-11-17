namespace DecentReadsApi.Entities
{
    public class FavoriteBook
    {
        public int Id { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
