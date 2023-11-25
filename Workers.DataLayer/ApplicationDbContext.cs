using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workers.Domain.Enum;
using Workers.Domain.Models;

namespace Workers.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Resume?> Resumes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}