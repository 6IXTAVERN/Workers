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
        public DbSet<Resume> Resumes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        // тест(уже есть одно резюме)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resume>(builder =>
            {
                builder.ToTable("Resumes").HasKey(x => x.Id);
                
                builder.HasData(new Resume
                {
                    Id = 1,
                    FirstName = "Aboba",
                    MiddleName = "Kylebyakov",
                    LastName = "Abobchikov",
                    DateCreated = DateTime.Now,
                    Faculty = Faculty.Economics
                });
            });
            
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        }
    }
}