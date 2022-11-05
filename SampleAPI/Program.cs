using System.Text.Json.Serialization;
using SampleAPI.Class;
using SampleAPI.Interface;
using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // 將 NULL 的項目都忽略掉
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "範例 API",
        Description = "測試 ASP.NET Core Web API 文件",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddScoped<IScoped, SampleClass>();
builder.Services.AddSingleton<ISingleton, SampleClass>();
builder.Services.AddTransient<ITransient, SampleClass>();
builder.Services.AddScoped<SampleService, SampleService>();

builder.Services.AddDbContext<BlogContext>(options =>
{
    options.EnableSensitiveDataLogging();
});
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
