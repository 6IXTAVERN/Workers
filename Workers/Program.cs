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
var getConnectionStringName = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
   options.UseMySql(getConnectionStringName, ServerVersion.AutoDetect(getConnectionStringName), b => b.MigrationsAssembly("Workers.DataLayer")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

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
