using Microsoft.AspNetCore.Mvc;
using SampleMinimal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogContext>(options =>
{
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// app.MapPut("/articles/{id}", async (http) =>
// {
//     if (!http.Request.RouteValues.TryGetValue("id", out var id))
//     {
//         http.Response.StatusCode = 400;
//         return;
//     }
//     var db = http.RequestServices.GetService<BlogContext>();
//     var article = await db.Articles.FindAsync(int.Parse(id.ToString()));

//     if (article == null)
//     {
//         http.Response.StatusCode = 404;
//         return;
//     }
//     var inputArticle = await http.Request.ReadFromJsonAsync<Article>();

//     article.Title = inputArticle.Title;
//     await db.SaveChangesAsync();
//     http.Response.StatusCode = 204;
// });

app.MapPut("/articles/{id}", async (int id, [FromBody]Article inputArticle, BlogContext db) =>
{
    var article = await db.Articles.FindAsync(id);

    if (article == null)
        return Results.NotFound();
    
    article.Content = inputArticle.Content;
    article.Title = inputArticle.Title;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
