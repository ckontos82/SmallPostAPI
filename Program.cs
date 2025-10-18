using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmallPostAPI.Data;
using SmallPostAPI.Services;
using SmallPostAPI.Services.Interfaces;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var payload = new
        {
            status = report.Status.ToString(),
            version = Assembly.GetExecutingAssembly().GetName().Version
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllers();

app.Run();
