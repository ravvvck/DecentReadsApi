using Microsoft.EntityFrameworkCore;

namespace DecentReadsApi.Entities
{
    public class DecentReadsDbContext : DbContext
    {
        public DecentReadsDbContext(DbContextOptions<DecentReadsDbContext> options): base(options)
        {

        }

        private string _connectionString = "Server=DESKTOP-DQ5BFKA\\SQLEXPRESS;Database=GoodreadsDb;Trusted_Connection=True";


        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);



            modelBuilder.Entity<Author>()
      .Property(e => e.FirstName)
      .IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Author>()
      .Property(e => e.LastName)
      .IsRequired().HasMaxLength(25);

            modelBuilder.Entity<User>()
          .Property(e => e.Email)
          .IsRequired();

            modelBuilder.Entity<Role>()
          .Property(e => e.Name)
          .IsRequired();

   
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
                
        }
    }
}
