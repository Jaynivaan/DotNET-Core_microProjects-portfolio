//gs
using Day22.InvoiceToCashLite.Features.Reconciliation.Interfaces;

namespace Day22.InvoiceToCashLite.Features.Reconciliation
{
    public static class ReconciliationEndpoints
    {
        public static void MapReconciliationEndpoints (this WebApplication app)
        {
            app.MapGet("/api/reconciliation", (IReconciliationService service) =>
            {
                var result = service.GetSummary();

                return Results.Ok(result);
            })
            .WithName("GetReconciliatoin")
            .WithSummary("Generate reconciliation Summary");
        }
    }
}