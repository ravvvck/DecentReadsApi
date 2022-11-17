using DecentReadsApi.Exceptions;

namespace DecentReadsApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {


        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

            }
            catch (BadRequestException badRequestex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestex.Message);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;

            }
            catch (AlreadyExistException alreadyExistException)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync("Already exist");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
