using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Helpers;

public static class SeedDataExtentions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("06158C72-3F2E-4D6F-8AF6-96D1354A77CE"),
                UserName = "ATai"
            });

        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 1,
                Title = "ASP.NET Core",
                Content = "第一次學ASP.NET Core就上手",
                PostTime = new DateTime(2021, 11, 24, 14, 30, 00),
                UserId = new Guid("06158C72-3F2E-4D6F-8AF6-96D1354A77CE")
            });
    }
}

