//gam sam//
using Day02_TodoAPI.Services;
using Day02_TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

//register service
builder.Services.AddSingleton<TodoService>();

var app = builder.Build();


//routes//

//get all todos
app.MapGet("/todos", (TodoService service) =>
{
    var todos = service.GetAll();
    
    return Results.Ok(new ApiResponse<List<TodoItem>>
    {
        Success = true,
        Message = "Todos fetched Successfully, Thanks!",
        Data = todos
    });

});


//get todo by id
app.MapGet("/todo/{id}", (int id, TodoService service) =>
{
    var todo = service.GetAll().FirstOrDefault(t => t.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();

});


//create todo
app.MapPost("/todo", (string title, TodoService service) =>
{
    var todo = service.Add(title);
    return Results.Ok(new ApiResponse<TodoItem>
    {
        Success = true,
        Message = "Todo created Successfully, Thanks!",
        Data = todo
    });

});


//update todo
app.MapPut("/todo/{id}", (int id, string title, bool isCompleted, TodoService service) =>
{
    var updated = service.Update(id, title, isCompleted);
    return updated ? Results.Ok("Updated") : Results.NotFound();
});

//delete todo
app.MapDelete("/todo/{id}", (int id, TodoService service) =>
{
    var deleted = service.Delete(id);
    if (!deleted)
    {
        return Results.NotFound(new ApiResponse<string>
        {
            Success = false,
            Message = "Todo not found",
            Data = null
        });
    }
    return Results.Ok(new ApiResponse<string>
    {
        Success = true,
        Message = "Todo deleted successfully",
        Data = null
    });
});


app.Run();