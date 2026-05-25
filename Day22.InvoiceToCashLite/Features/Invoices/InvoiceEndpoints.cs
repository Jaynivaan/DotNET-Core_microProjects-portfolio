//gs
using Day22.InvoiceToCashLite.Features.Invoices.Interfaces;
using System;
using System.CodeDom.Compiler;

namespace Day22.InvoiceToCashLite.Features.Invoices
{
    public static class InvoiceEndpoints
    {
        public static void MapInvoiceEndpoints(this WebApplication app)
        {
            app.MapPost("/api/invoices",(CreateInvoiceRequest request, IInvoiceService service) =>
            {
                var result = service.CreateInvoice(request);

                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
             .WithName("CreateInvoice")
             .WithSummary("Create a new Invoice");

            app.MapGet("/api/invoices", (IInvoiceService service) =>
            {
                var result = service.GetAllInvoices();

                return Results.Ok(result);
            })
             .WithName("Get Invoices")
             .WithSummary("Get all Invoices");


            app.MapGet("/api/invoices/{id}", (Guid id, IInvoiceService service) =>
            {
                var result = service.GetInvoiceById(id);

                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            })
            .WithName("GetInvoiceById")
            .WithSummary("Get Invoice by id");

            app.MapPost("/api/invoices/{id}/cancel", (Guid id, IInvoiceService service) =>
            {
                var result = service.CancelInvoice(id);

                return result.Success
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("CancelInvoice")
            .WithSummary("Cancel an invoice");

        }
    }
}