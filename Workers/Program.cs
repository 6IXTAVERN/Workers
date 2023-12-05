using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workers.DataLayer;
using Workers.DataLayer.Interfaces;
using Workers.DataLayer.Repositories;
using Workers.Domain.Models;
using Workers.Services.Implementations;
using Workers.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

// MySQL Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("IdentityDemo"));

builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<ApplicationDbContext>();

// регистрация интерфейсов
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

// регистрация сервисов
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// block unused Identity
app.Map("/Identity/Account/ForgotPassword", HandleRequest);
app.Map("/Identity/Account/ResendEmailConfirmation", HandleRequest);

static void HandleRequest(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("404 Not Found");
    });
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "about",
    pattern: "{controller=Home}/{action=About}/{id?}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
