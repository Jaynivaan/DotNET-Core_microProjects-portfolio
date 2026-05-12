//gs
using Day12.SearchAPI.Models;
using System;
using System.Collections.Generic;

namespace Day12.SearchAPI.Data
{
    //this class inserts initial sample data into db
    //why???
    //because empty database is not searchable ....
    //seeding techniqu4e help to simulate real searchable content


    public static class DatabaseSeeder
    {
        //This method checks db and inserts sample docs
        public static void Seed(SearchDbContext dbContext)
        {
            //if document already exist stop seeding.
            //prevents duplicate data insertion on every app start.
            if(dbContext.Documents.Any())
            {
                return;
            }

            //sample searchable documents
            var documents = new List<SearchDocument>
            {
                new SearchDocument
                {
                    Title = "MyResume",
                    Content = "Brandnew DOTNET_DEVELOPER.learing the skill to develop a personal app recepie standard",
                    Category = "Resume",
                    Tags = "resume, dotnet, csharp, aspnetcore,sqlserver,blazor,backend, still learining",

                    Source = @"C:\portfolio\\dotnet-core-microprojects\testprops\JAYAKRISHNAN PARAMESWARANKUTTY-resume.docx",
                    Created = DateTime.UtcNow
                }
            };
            dbContext.Documents.AddRange(documents);
            dbContext.SaveChanges();

        }
    }
}