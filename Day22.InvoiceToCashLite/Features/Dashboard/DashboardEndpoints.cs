//gs
using Day22.InvoiceToCashLite.Features.Dashboard.Interfaces;

namespace Day22.InvoiceToCashLite.Features.Dashboard
{
    public static class DashboardEndpoints
    {
        public static void MapDashboardEndpoints (this WebApplication app)
        {
            app.MapGet("/api/dashboard", (IDashboardService service) =>
            {
                var result = service.GetDashboard();

                return Results.Ok(result);

            })
            .WithName("GetDashboard")
            .WithSummary("Get invoice-to-cash dashboard summary");
        }
    }
}