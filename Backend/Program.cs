using Backend.Services;
using Backend.Models;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ZstdSharp.Unsafe;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<TodoService>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "To-Do API", Version = "v1" });
});




var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "To-Do API v1");
    });
}

app.UseCors("AllowReactApp");


app.MapGet("/gTodos", async (MongoDbService mongo) =>
{
    var collection=mongo.Database.GetCollection<TodoItem>("ToDo");
    var todos = await collection.Find(_ => true).ToListAsync();
    return Results.Ok(todos);
});

app.MapPost("/pTodos", async (MongoDbService mongo, TodoItem todo) =>
{
    if (string.IsNullOrEmpty(todo.Id) || !ObjectId.TryParse(todo.Id, out _))
    {
        todo.Id = null;
    }
   var collection = mongo.Database.GetCollection<TodoItem>("ToDo");
   await collection.InsertOneAsync(todo);
   return Results.Created($"/todos/{todo.Id}", todo);
});
app.Run();