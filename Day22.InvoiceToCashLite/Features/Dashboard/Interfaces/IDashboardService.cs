//gs
using Day22.InvoiceToCashLite.Shared;

namespace Day22.InvoiceToCashLite.Features.Dashboard.Interfaces
{
    public interface IDashboardService
    {
        ApiResponse<DashboardResponse> GetDashboard(); 

    }
}