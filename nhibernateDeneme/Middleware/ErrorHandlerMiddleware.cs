using System.Net;
using System.Text.Json;

namespace nhibernateDeneme.Middleware;
using Microsoft.AspNetCore.Http;
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
            await _next.Invoke(context);

        }
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application.json";

            var messageError = string.Empty;

            messageError = "Interval Server Error";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(messageError);
            await response.WriteAsync(result);
        }
    }

}