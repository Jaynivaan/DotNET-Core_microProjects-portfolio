//gs
using Day22.InvoiceToCashLite.Features.Invoices.Interfaces;
using System;

namespace Day22.InvoiceToCashLite.Features.Invoices
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this WebApplication app)
        {
            app.MapPost("/invoices",(CreateInvoiceRequest request, IInvoiceService service) =>
            {
                var result = service.CreateInvoice(request);

                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
             .WithName("CreateInvoice")
             .WithSummary("Create a new Invoice");

            app.MapGet("/invoices", (IInvoiceService service) =>
            {
                var result = service.GetAllInvoices();

                return Results.Ok(result);
            })
             .WithName("Get Invoices")
             .WithSummary("Get all Invoices");


            app.MapGet("/invoices/{id:guid}", (Guid id, IInvoiceService service) =>
            {
                var result = service.GetInvoiceById(id);

                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            })
            .WithName("GetInvoiceById")
            .WithSummary("Get Invoice by id");
        }
    }
}