using LMS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace LMS.DataAccess;


public class AppDbContext:IdentityDbContext<IdentityUser> 
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Category>? Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.ModelBuilder(modelBuilder);
        
        modelBuilder.Entity<Book>().HasKey(u => u.Id);
        modelBuilder.Entity<Book>().Property(u => u.Title).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Category>().HasKey(u => u.Id);
        modelBuilder.Entity<Book>().HasOne(u => u.Category)
            .WithMany(u => u.Book)
            .HasForeignKey(u => u.CategoryId);

        modelBuilder.Entity<Book>().Navigation(u => u.Category).AutoInclude();




    }
}

