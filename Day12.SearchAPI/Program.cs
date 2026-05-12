//gs
using Day12.SearchAPI.Config;
using Day12.SearchAPI.Data;
using Day12.SearchAPI.Endpoints;
using Day12.SearchAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//database registration
//EF Core will use Sql server through this dbcontext.
builder.Services.AddDbContext<SearchDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//Config  binding
//we added a default setting within SearchOptions.cs file inside config but appsettings.json will override that default values.

builder.Services.Configure<SearchOptions>(
    builder.Configuration.GetSection("SearchOptions")
);

//service registration
//endpoints depends on ISearchService not on HybridSearchservice directly...
//DI provides HybridSearchService

builder.Services.AddScoped<ISearchService, HybridSearchService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SearchDbContext>();
    DatabaseSeeder.Seed(dbContext );

}

    //map endpoints

    app.MapSearchEndpoints();

//start the app

app.Run();
