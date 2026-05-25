//gs

using Day22.InvoiceToCashLite.Shared;


namespace Day22.InvoiceToCashLite.Features.Reconciliation.Interfaces
{
    public interface IReconciliationService
    {
        ApiResponse<ReconciliationResponse>
            GetSummary();
    }
}