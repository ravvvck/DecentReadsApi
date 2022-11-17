using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DecentReadsApi.Entities;
using DecentReadsApi.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization.Policy;
using DecentReadsApi.Tests.Helpers;
using DecentReadsApi.Controllers;

namespace DecentReadsApi.Tests
{ 

    public class BooksControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _client;
        /*private WebApplicationFactory<Program> _factory;*/

        public BooksControllerTests(WebApplicationFactory<Program> factory)
        {

            _client=
            factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<GoodreadsDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<GoodreadsDbContext>(options => options.UseInMemoryDatabase("GoodreadsDb"));
                    });
                }).CreateClient();


        }
        [Fact]
        public async Task GetAll_NoParameters_ReturnsOkResult()
        {

            var response = await _client.GetAsync("/api/books");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task Create_WithValidModel_ReturnsCreatedStatus()
        {

            var model = new CreateBookDto()
            {
                Name = "TestBook",
                AuthorFirstName = "FirstName",
                AuthorLastName = "LastName",
                Description = "Description"
            };
            var httpContent = model.ToJsonHttpContent();

            var response = await _client.PostAsync("api/books", httpContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();

        }

        [Fact]
        public async Task Create_WithInvalidModel_ReturnsBadRequest()
        {

            var model = new CreateBookDto()
            {
                
                AuthorFirstName = "FirstName",
                AuthorLastName = "LastName",
                Description = "Description"
            };
            var httpContent = model.ToJsonHttpContent();

            var response = await _client.PostAsync("api/books", httpContent);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        }

       /* [Fact]
        public async Task Delete_ForExisting_ReturnsNotFound()
        {
            var book = new Book()
            {
                Name = "Test",
                PublishedDate = DateTime.Parse("1997-06-26"),
                NumberOfPages = 250,
                Description = "Description",
                Author = new Author()
                {
                    FirstName = "A",
                    LastName = "A"
                }

           
            };

            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using IServiceScope? scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<GoodreadsDbContext>();
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            var response = await _client.DeleteAsync("api/books/" + book.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }*/

        [Fact]
        public async Task Delete_ForNonExisting_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("api/books/999");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }
    }
}
