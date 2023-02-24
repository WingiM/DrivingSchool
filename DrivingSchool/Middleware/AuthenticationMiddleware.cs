using System.Collections.Concurrent;
using DrivingSchool.Data;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Middleware;

public class AuthenticationMiddleware
{
    public static IDictionary<Guid, LoginCredentials> Logins { get; private set; }
        = new ConcurrentDictionary<Guid, LoginCredentials>();


    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, SignInManager<IdentityUser<int>> signInManager)
    {
        if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]!);
            var info = Logins[key];

            var result = await signInManager.PasswordSignInAsync(info.Email, info.Password,
                info.RememberMe, lockoutOnFailure: false);
            info.Password = string.Empty;
            if (result.Succeeded)
            {
                Logins.Remove(key);
                context.Response.Redirect("/");
            }
        }
        else if (context.Request.Path == "/logout")
        {
            await signInManager.SignOutAsync();
            context.Response.Redirect("/login");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}