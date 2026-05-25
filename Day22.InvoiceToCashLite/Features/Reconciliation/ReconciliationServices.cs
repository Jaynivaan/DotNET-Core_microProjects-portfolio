//gs
//data,inter,shared
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Shared;
using Day22.InvoiceToCashLite.Features.Reconciliation.Interfaces;

namespace Day22.InvoiceToCashLite.Features.Reconciliation
{
    public class ReconciliationService : IReconciliationService
    {
        private readonly InMemoryStore _store;

        public ReconciliationService(InMemoryStore store)
        {
            _store = store;
        }

        public ApiResponse<ReconciliationResponse>
            GetSummary()
        {
            var summary =
                new ReconciliationResponse
                {
                    TotalInvoices = _store.Invoices.Count,

                    TotalPayments = _store.Payments.Count,

                    TotalInvoiced = _store.Invoices.Sum(x => x.Amount),

                    TotalCollected = _store.Payments.Sum(x => x.Amount),

                    OutstandingBalance = _store.Invoices.Sum(x => x.BalanceDue)
                };

            return new ApiResponse<ReconciliationResponse>
            {
                Success = true,

                Message = "Reconciliation generated successfully",

                Data = summary
            };
        }

    }
}