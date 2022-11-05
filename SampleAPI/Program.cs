using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Rewrite;
using SampleAPI.Class;
using SampleAPI.Filters;
using SampleAPI.Interface;
using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPI.Services;
using Serilog;
using Serilog.Events;

// 初始化 Serilog
// https://github.com/serilog/serilog/wiki/Configuration-Basics
Log.Logger = new LoggerConfiguration()
            // 將紀錄最小層級設定在 Verbose
            .MinimumLevel.Verbose()
            // 覆寫原有的設定
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            // 讓所有紀錄事件加入特定的屬性，需要加上這行後續才能使用 LogContext
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5431")
            // 輸出的檔案類型與位置，可以加入 rollingInterval 設定在檔名的末端加入時間
            .WriteTo.File("./Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

try
{
    Log.Information("開始啟動 ASP.NET Core 應用程式");

    var builder = WebApplication.CreateBuilder(args);

    // 讓 Host 使用 Serilog 
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
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

        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

        options.DocumentFilter<HiddenAPIFilter>();
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

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://www.sample.com", "http://www.demo.com")
                // 設定字網域成為原始的來源
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                // 允許所有 Request Header
                .AllowAnyHeader()
                // 允許所有 Http Method
                .AllowAnyMethod();
        });

        options.AddPolicy(
            name: "CustomPolicy1",
            builder =>
            {
                builder.WithOrigins("http://www.sample.com", "http://www.demo.com");
            }
        );

        options.AddPolicy(
            name: "CustomPolicy2",
            builder =>
            {
                builder.WithOrigins("http://www.homepage.com");
            }
        );
    });

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
    else
    {
        // app.UseExceptionHandler("/error");

        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;

                await context.Response.WriteAsync("拋出一個Exception");

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                {
                    await context.Response.WriteAsync("找不到檔案");
                }

                if (exceptionHandlerPathFeature?.Path == "/")
                {
                    await context.Response.WriteAsync(" Page: Home.");
                }
            });
        });
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseCors("CustomPolicy1");

    // 每一個 Request 使用 Serilog 記錄下來
    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.UseRewriter(new RewriteOptions()
        .AddRewrite("Post.aspx", "WeatherForecast", skipRemainingRules: true)
        .AddRedirect("Post.php", "WeatherForecast", 301)
        .AddRedirect("WeatherForecast/(.*)/(.*)", "WeatherForecast?id=$1&id2=$2", 301));

    app.MapGet("/ep1", [EnableCors("allowAny")] (HttpContext context) =>
    {
        return "hello ep1";
    });

    app.MapGet("/ep2", (HttpContext context) =>
    {
        return "hello ep2";
    }).RequireCors("CustomPolicy1");

    app.Run();
}
catch (System.Exception ex)
{
    Log.Fatal(ex, "啟動失敗！");
}
finally
{
    Log.CloseAndFlush();
}

