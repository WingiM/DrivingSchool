using DrivingSchool.Domain.Exceptions;

namespace DrivingSchool.BlazorWebClient.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (NotFoundException)
        {
            context.Response.Redirect("/notfound");
        }
        catch (Exception e)
        {
            _logger.LogError("Error: {Error}", e);  
        }
    }
}