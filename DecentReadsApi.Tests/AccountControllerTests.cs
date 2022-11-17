using FluentAssertions;
using DecentReadsApi.Entities;
using DecentReadsApi.Models;
using DecentReadsApi.Tests.Helpers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace DecentReadsApi.Tests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _client;

        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DecentReadsDbContext>));
                        services.Remove(dbContextOptions);

                        

                        services.AddDbContext<DecentReadsDbContext>(options => options.UseInMemoryDatabase("GoodreadsDb"));
                    });
                }).CreateClient();
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            var registerUser =new RegisterUserDto()
            {
                Email = "test@test.com",
                Username = "test",
                Password = "testPassword",
                ConfirmPassword = "testPassword"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            var response = await _client.PostAsync("api/account/register", httpContent);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidModel_ReturnsBadRequest()
        {
            var registerUser = new RegisterUserDto()
            {
                
                Username = "test",
                Password = "testPasswor",
                ConfirmPassword = "testPassword"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            var response = await _client.PostAsync("api/account/register", httpContent);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }











    }
}
