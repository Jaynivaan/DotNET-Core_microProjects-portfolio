//gs//

using Day03_AuthSystem.Services;
using Day03_AuthSystem.Models;

var builder = WebApplication.CreateBuilder(args);

//Register the AuthService as a singleton
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

//------auth endpoints----//------auth endpoints----/   

//Register endpoint
//this endpoint will receive a username and password from the client, and it will call the Register method of the AuthService to create a new user.
////It will return an ApiResponse<User> object that contains the result of the registration process.


app.MapPost("/register", (string username, string password, AuthService service) =>
{
   //call service logic 
   var result = service.Register(username, password);


   //return structured response to cient
   return Results.Ok(result);

});

//Login endpoint
//validate user credentials and return a token if successful

app.MapPost("/login", (string username, string password, AuthService service) =>
{ 
    var result = service.Login(username, password);
    return Results.Ok(result);
});

//protected endpoint

app.MapGet("/secret", (HttpRequest request, AuthService service) =>
{
    //read token from request header
    var token = request.Headers["Authorization"].ToString();

    //validate token
    if (!service.IsTokenValid(token))
    {
        //if invalid reject access
        return Results.Unauthorized();
    }
    //IF VALID GRANT ACCESS TO PROTECTED RESOURCE
    return Results.Ok(new ApiResponse<string>
    {
        Success = true,
        Message = "Welcome to the secret area!",
        Data = "you are authenticated and authorized to access this resource. Thanks you for coming this far."
    });
});

//start the application

app.Run();
