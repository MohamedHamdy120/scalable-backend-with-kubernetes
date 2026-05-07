using UnitsApi.Services;
using UnitsApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PostService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/", () => Results.Ok("Units API is running"));

app.MapGet("/health", () => Results.Ok("OK"));

app.MapGet("/posts", async (PostService service) =>
{
    return await service.GetPost();
});

app.MapPost("/posts", async (Post post, PostService service) =>
{
    await service.CreatePost(post);
    return Results.Ok(post);
});

app.MapGet("/posts/{id}", async (string id, PostService service) =>
{
    var post = await service.GetPostById(id);

    return post is not null
        ? Results.Ok(post)
        : Results.NotFound();
});

app.MapPut("/posts/{id}", async (
    string id,
    Post updatedPost,
    PostService service) =>
{
    var updated = await service.UpdatePost(id, updatedPost);

    return updated
        ? Results.Ok(updatedPost)
        : Results.NotFound();
});

app.MapDelete("/posts/{id}", async (
    string id,
    PostService service) =>
{
    var deleted = await service.DeletePost(id);

    return deleted
        ? Results.Ok()
        : Results.NotFound();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}