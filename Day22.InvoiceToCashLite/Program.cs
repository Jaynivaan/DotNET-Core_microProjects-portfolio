//gs
using Day22.InvoiceToCashLite.Extensions;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddRazorComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAntiforgery();
app.MapRazorComponents<App>();

app.MapInvoiceEndpoints();

app.Run();