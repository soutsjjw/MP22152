using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Helpers;

namespace SampleAPI.Models;

public class BlogContext : DbContext
{
    private readonly IConfiguration _config;

    public BlogContext(IConfiguration config)
    {
        _config = config;
    }

    public BlogContext(DbContextOptions<BlogContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Blog;User Id=sa;Password=P@ssw0rd;");
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnnection"]);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }

    public virtual DbSet<Article> Articles { get; set; } = default!;
    public virtual DbSet<User> Users { get; set; } = default!;
}
