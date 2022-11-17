using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DecentReadsApi.Tests
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Role, "Librarian"),
                }
                ));
          context.HttpContext.User = claimsPrincipal;
            await next();
        }
    }
}
