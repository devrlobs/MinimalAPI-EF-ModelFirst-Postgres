using Microsoft.EntityFrameworkCore;
using MinimalAPI_EF_Postgres.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration["PostgresDBConnection"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/inserttodo", async (DataContext db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{todo.Id}", todo);
})
.WithName("inserttodo")
.WithOpenApi();

app.MapGet("/gettodo", async (DataContext db) =>
{
    var todolist = await db.Todos.ToListAsync();
    return todolist;
})
.WithName("gettodo")
.WithOpenApi();


app.MapPut("/updatetodo/{id}", async (DataContext db, int id, Todo todo) =>
{
    var todoToUpdate = await db.Todos.FindAsync(id);
    if (todoToUpdate is null) return Results.NotFound();

    todoToUpdate.Name = todo.Name;
    todoToUpdate.IsDone = todo.IsDone;
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("updatetodo")
.WithOpenApi();


app.MapDelete("/deletetodo/{id}", async (DataContext db, int id) =>
{
    var userToDelete = await db.Todos.FindAsync(id);
    if (userToDelete is not null)
    {
        db.Todos.Remove(userToDelete);
        await db.SaveChangesAsync();
    }
    return Results.NotFound();
})
.WithName("deletetodo")
.WithOpenApi();

app.Run();
