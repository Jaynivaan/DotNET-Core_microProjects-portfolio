//gs
using Day22.InvoiceToCashLite.Features.Dashboard.Interfaces;
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Shared;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Reconciliation;

namespace Day22.InvoiceToCashLite.Features.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly InMemoryStore _store;

        public DashboardService(InMemoryStore store)
        {
            _store = store;
        }

        public ApiResponse<DashboardResponse> GetDashboard()
        {
            var response = new DashboardResponse
            {
                Invoices =
                    _store.Invoices
                        .Select(x => new InvoiceResponse
                        {
                            Id = x.Id,

                            CustomerName = x.CustomerName,

                            Amount = x.Amount,

                            BalanceDue = x.BalanceDue,

                            Status = x.Status.ToString()
                        })
                        .ToList(),
                Payments =
                    _store.Payments,

                Reconciliation =
                    new ReconciliationResponse
                    {
                        TotalInvoices = _store.Invoices.Count,

                        TotalPayments = _store.Payments.Count,

                        TotalInvoiced = _store.Invoices.Sum(x => x.Amount),

                        TotalCollected = _store.Payments.Sum(x => x.Amount),

                        OutstandingBalance = _store.Invoices.Sum(x => x.BalanceDue)

                    }
            };

            return new ApiResponse<DashboardResponse>
            {
                Success = true,

                Message = "Dashboard Loaded",

                Data = response
            };
        }
    }
}