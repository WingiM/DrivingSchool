using DrivingSchool.Domain.Exceptions;

namespace DrivingSchool.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            context.Response.Redirect("/notfound");
        }
    }
}