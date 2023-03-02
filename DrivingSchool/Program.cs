using DrivingSchool;
using DrivingSchool.Data;
using DrivingSchool.Domain;
using DrivingSchool.Middleware;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => 
    loggerConfiguration
        .ReadFrom
        .Configuration(hostingContext.Configuration)
        .WriteTo.Console()
);
builder.Services.AddServerSideBlazor();
builder.Services.AddData(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddDefaultIdentity<IdentityUser<int>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddMudServices();
builder.Services
    .AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();