//gs
using Day07_CRUD_App.Models;
using Day07_CRUD_App.Responses;
using Day07_CRUD_App.Services;
using Day07_CRUD_App;

var builder = WebApplication.CreateBuilder(args);

//registering razor components/Blazor services
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


//register Todo service in dependency injection container.
//singleton scope means one shared in-memory list while app is running.

builder.Services.AddScoped<ITodoService, TodoService>();

var app  = builder.Build();

//get all todos

app.MapGet("/todos", (ITodoService service) =>
{
    var todos = service.GetAll();

    return Results.Ok(new ApiResponse<List<TodoItem>>
    {
        Success = true,
        Message = "All Todos fetched Successfully.",
        Data = todos

    });
});

//get todo by id

app.MapGet("/todos/{id}", (int id, ITodoService service) =>
{
    var todo = service.GetById(id);

    if (todo is null)
    {
        return Results.NotFound(new ApiResponse<TodoItem>
        {
            Success = false,
            Message = "Todo not found",
            Data = null
        });
    }
    return Results.Ok(new ApiResponse<TodoItem>
    {
        Success = true,
        Message = "Requested Todo fetched Successfully",
        Data = todo
    });
});

//create Todo

app.MapPost("/todos", (string title, ITodoService service) =>
{
    if (string.IsNullOrWhiteSpace(title))
    {
        return Results.BadRequest(new ApiResponse<TodoItem>
        {
            Success = false,
            Message = "Title cannot be empty",
            Data = null
        });
    }

    var todo = service.Create(title);
    return Results.Ok(new ApiResponse<TodoItem>
    {
        Success = true,
        Message = "Todo created Successfully",
        Data = todo
    });

});

//update Todo
app.MapPut("/todos/{id}", ( int id, string title, bool isCompleted, ITodoService service) =>
{
    var updated = service.Update(id, title, isCompleted);

    if (!updated)
    {
        return Results.NotFound(new ApiResponse<TodoItem>
        {
            Success = false,
            Message = "Todo not found",
            Data = null
        });
    }

    return Results.Ok(new ApiResponse<string>
    {
        Success = true,
        Message = "Todo Updated Success fully",
        Data = "updated"
    });

});

//delete todo
app.MapDelete("/todos/{id}", (int id, ITodoService service) =>
{
    var deleted = service.Delete(id);

    if (!deleted)
    {
        return Results.NotFound(new ApiResponse<TodoItem>
        {
            Success = false,
            Message = "Todo not found",
            Data = null
        });
    }
    return Results.Ok(new ApiResponse<string>
    {
        Success = true,
        Message = $"Todo {id} has been deleted.",
        Data = "deleted"
    });
});

app.UseAntiforgery();

//enabling razor componenet pages
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
//start app
app.Run();