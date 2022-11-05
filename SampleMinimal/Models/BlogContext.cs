using Microsoft.EntityFrameworkCore;
using SampleMinimal.Helpers;

namespace SampleMinimal.Models;

public class BlogContext : DbContext
{
    public BlogContext() { }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Blog;User Id=sa;Password=P@ssw0rd;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }

    public virtual DbSet<Article> Articles { get; set; } = default!;
    public virtual DbSet<User> Users { get; set; } = default!;
}
