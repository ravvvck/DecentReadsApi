using DecentReadsApi.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DecentReadsApi.Verification
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Book>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Book book)
        {
            /*if (requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            if (book.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }*/

            return Task.CompletedTask;
        }
    }
}
