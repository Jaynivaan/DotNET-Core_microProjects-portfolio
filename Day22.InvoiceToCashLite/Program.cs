//gs
using Day22.InvoiceToCashLite.Extensions;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Components;
using Day22.InvoiceToCashLite.Features.Payments;
using Day22.InvoiceToCashLite.Features.Reconciliation;
using Day22.InvoiceToCashLite.Features.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapInvoiceEndpoints();
app.MapPaymentEndpoints();
app.MapReconciliationEndpoints();
app.MapDashboardEndpoints();

app.Run();