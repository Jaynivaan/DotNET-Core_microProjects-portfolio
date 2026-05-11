//gs
using Day11.BackgroundJobs.Endpoints;
using Day11.BackgroundJobs.Services;

//this program.cs is the orchestration layer of this application.
//nervous system wiring.

var builder = WebApplication.CreateBuilder(args);

//===============================================================
//Dependency injection Registration
//==============================================================

//Singleton
//One shared memory state across whole application lifetime.

builder.Services.AddSingleton<IJobStatusService, JobStatusService>();

//================
//hosted background worker
//===================

//framework automatically starts this worker.

builder.Services.AddHostedService<BackgroundPulseWorker>();

var app = builder.Build();

//==========================
//Endpoint Mapping
//==========
app.MapJobStatusEndpoints();

//========================================
//Run application
//==================
app.Run();


//gs
