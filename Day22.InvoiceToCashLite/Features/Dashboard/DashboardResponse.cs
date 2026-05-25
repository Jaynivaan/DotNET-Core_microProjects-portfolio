//gs
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Payments;
using Day22.InvoiceToCashLite.Features.Reconciliation;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Features.Dashboard
{
    public class DashboardResponse
    {
        public List<InvoiceResponse> Invoices { get; set; } = new();

        public List<Payment> Payments { get; set; } = new();

        public ReconciliationResponse? Reconciliation { get; set; }
    }
}