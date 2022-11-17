using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace DecentReadsApi.Tests
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var ticket = new AuthenticationTicket(claimsPrincipal, "Test");
            var authResult = AuthenticateResult.Success(ticket);

            return Task.FromResult(authResult);
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            var authResult = PolicyAuthorizationResult.Success();
            return Task.FromResult(authResult);
        }
    }
}
