//gs
using Day04_FileUploadAPI.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

//----------------------------------------------------
//    Step1: Define the Uploads Folder
//----------------------------------------------------

//get the current directory
var rootPath = Directory.GetCurrentDirectory();

//combine it with "uploads" to get the full path (absolute path) to the uploads folder
var uploadsFolder = Path.Combine(rootPath, "Uploads");

//Create the uploads folder if it doesn't exist
Directory.CreateDirectory(uploadsFolder);

//----------------------------------------------------
//step2: Upload Endpoint
//----------------------------------------------------

//This endpoint acceps a file from a user
app.MapPost("/Upload", async (IFormFile file) =>
{
    // check if the file is not null and has content
    if(file == null || file .Length == 0)
    {
        return Results.BadRequest(new ApiResponse<string>
        {
            Success = false,
            Message = "No file uploaded or file is empty.",
            Data = null
        });
    }
    //safety: Extract only file name to prevent path traversal attacks
    var safeFileName = Path.GetFileName(file.FileName);

    //full path to save file.
    var filePath = Path.Combine(uploadsFolder, safeFileName);

    //save file
    using var stream = new FileStream(filePath, FileMode.Create);
    await file.CopyToAsync(stream);

    //Success response.

    return Results.Ok(new ApiResponse<object>
    {
                Success = true,
                Message = "File uploaded successfully.",
                Data = new
                {
                    fileName = safeFileName,
                    size =file.Length,
                    path = filePath
                }
    });
})
.DisableAntiforgery();

//----------------------------------------------------
//step3: List Uploaded Files Endpoint
//----------------------------------------------------

//return list of Uploaded files
app.MapGet("/files", () =>
{
    //Read all files in the uploads folder
    var files = Directory.GetFiles(uploadsFolder)
        .Select(file => new
        {
            fileName = Path.GetFileName(file),
            size = new FileInfo(file).Length
        })
        .ToList();
    return Results.Ok(new ApiResponse<object>
    {
        Success = true,
        Message = "Files fetched successfully.",
        Data = files
    });
});

//step4: run the app
app.Run();
