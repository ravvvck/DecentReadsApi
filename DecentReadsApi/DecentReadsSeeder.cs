using DecentReadsApi.Entities;

namespace DecentReadsApi
{
    public class DecentReadsSeeder
    {
        private readonly DecentReadsDbContext dbContext;

        public DecentReadsSeeder(DecentReadsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
           

            if (dbContext.Database.CanConnect())
            {

                if (!dbContext.Roles.Any())
                {
                    var roles = new List<Role>()
                    {
                        new Role()
                        {
                            Name = "User"
                        },
                        new Role()
                        {
                            Name = "Librarian"
                        },
                        new Role()
                        {
                            Name = "Admin"
                        },
                    };
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }



                    if (!dbContext.Books.Any())
                {
                    var books = new List<Book>()
                    {
                        new Book()
                        {
                            Name = "Harry Potter and the Philosopher's Stone",
                            PublishedDate = DateTime.Parse("1997-06-26"),
                            NumberOfPages = 250,
                            Description = "Harry Potter thinks he is an ordinary boy - until he is rescued by an owl, " +
                            "taken to Hogwarts School of Witchcraft and Wizardry, " +
                            "learns to play Quidditch and does battle in a deadly duel." +
                            " The Reason ... HARRY POTTER IS A WIZARD!",
                            Author = new Author()
                            {
                                FirstName = "J. K. ",
                                LastName = "Rowling"
                            }
                            
                            
                        },
                        new Book()
                        {
                            Name = "Lord of the Rings: The Fellowship of the Ring",
                            PublishedDate = DateTime.Parse("1954-06-26"),
                            NumberOfPages = 250,
                            Description = "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkeness bind them" +
                            "In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, The Dark Lord, forged the One Ring,"+
                            "filling it with his own power so that he could rule all others. But the One Ring was taken from him," +
                            "and though he sought it throughout Middle-earth, "+
                            "it remained lost to him.",
                            Author = new Author()
                            {
                                FirstName = "J. R. R. ",
                                LastName = "Tolkien"
                            }


                        }
                    };
                    dbContext.Books.AddRange(books);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
