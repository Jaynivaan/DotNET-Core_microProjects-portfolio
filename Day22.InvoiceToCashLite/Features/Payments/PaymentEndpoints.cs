//gs
using Day22.InvoiceToCashLite.Features.Payments.Interfaces;

namespace Day22.InvoiceToCashLite.Features.Payments
{
    public static class PaymentEndpoints
    {
        public static void MapPaymentEndpoints(this WebApplication app)
        {
            app.MapPost("/api/payments",
                (CreatePaymentRequest request, IPaymentService service) =>
                {
                    var result = service.CreatePayment(request);

                    return result.Success
                        ? Results.Ok(result)
                        : Results.BadRequest(result);
                })
                .WithName("CreatePayment")
                .WithSummary("Submit a payment for an Invoice");


            app.MapGet("api/payments",
                (IPaymentService service) =>
                {
                    var result = service.GetAllPayments();

                    return Results.Ok(result);
                })
                .WithName("GetPayments")
                .WithSummary("Geta all Payments");


        }
    }
}
