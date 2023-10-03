using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using WebShopDQ.App.Common.Exceptions;

namespace WebShopDQ.App.Common
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    AppException => (int)HttpStatusCode.BadRequest,
                    DuplicateException => (int)HttpStatusCode.Conflict,// duplicatte error
                    PasswordException => (int)HttpStatusCode.BadRequest,// missing field error
                    ValidateException => (int)HttpStatusCode.BadRequest, // validate error
                    MissingFieldException => (int)HttpStatusCode.BadRequest,// missing field error
                    UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,// not found error
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };
                var result = JsonSerializer.Serialize(new { message = error?.Message, statusCode = response.StatusCode });
                await response.WriteAsync(result);
            }
        }
    }
}
