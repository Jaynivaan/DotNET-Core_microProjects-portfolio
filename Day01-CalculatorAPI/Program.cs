//Entry point for the application
var builder = WebApplication.CreateBuilder(args);

//build the app ie configuring services and data flow pipeline
var app = builder.Build();

//Root endpoint =>> simple health check
app.MapGet("/", () => "Day01 calculator api is running...");

//additon
//example:  /add?a=5&b=3
//query parameters ( a, b) are automatically bound from url
app.MapGet("/add", (double a, double b) =>
{
    double result = a + b;

    //return structured json response.
    return Results.Ok(new
    {
        succuss = true,
        message ="Computer computed the computational value,,Thanks a lot for your valuable time and energy checking this out ",
        operation = "addition",
        inputA = a,
        inputB = b,
        result = result
    });
});

//subtraction
app.MapGet("/subtract", (double a, double b) =>
{
    double result = a - b;

    //return structured json response.
    return Results.Ok(new
    {
        succuss = true,
        message = "Computer computed the computational value,,Thanks a lot for your valuable time and energy checking this out ",
        operation = "subtraction",
        inputA = a,
        inputb = b,
        result = (double)result
    });

});

//multiplication
app.MapGet("/multiply", (double a, double b) =>
{
    double result = a * b; 

    //return structured json response.
    return Results.Ok(new
    {
        succuss = true,
        message = "Computer computed the computational value,,Thanks a lot for your valuable time and energy checking this out ",
        operation = "multiplication",
        inputA = a,
        inputB = b,
        result = result

    });
});

//division
app.MapGet("/division", (double a, double b) =>
{
    double result = a / b;

    //return structured json response
    return Results.Ok(new
    {
        succuss = true,
        message = "Computer computed the computational value,,Thanks a lot for your valuable time and energy checking this out ",
        operation = "division",
        inputA = a,
        inputB = b,
        result = result
    });
});









//start the application
app.Run();
