//gs
using Microsoft.EntityFrameworkCore;
using Day12.SearchAPI.Models;

namespace Day12.SearchAPI.Data
{
    //Db context is the bridge between c# objects and sql Server
    //
    //SearchDocument class -> DbContext translates it => SqlTable

    //EF Core Watches this Models
    // and builds db structure from them
    //
    //DbContext is not business logic
    //it is persistence infrastructure.
    //
    // SRP: database communication
    public class SearchDbContext : DbContext
    {
        //Constructor receives DbContextOptions as we have config module
        //
        //as congiguration should come from outside..
        //(connection string , provider etc)
        //DI give this settings automatically/
        public SearchDbContext(DbContextOptions<SearchDbContext> options)
            : base(options)
        {

        }
        //This becomes a sql table
        //
        //DB Set<SearchDocument>
        //
        //EFCore , track and store SearchDocument entities
        public DbSet<SearchDocument> Documents { get; set; }
    }
}