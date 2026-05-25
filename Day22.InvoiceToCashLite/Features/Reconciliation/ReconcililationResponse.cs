//gs
namespace Day22.InvoiceToCashLite.Features.Reconciliation
{
    public class ReconciliationResponse
    {
        public int TotalInvoices { get; set; }

        public int TotalPayments { get; set; }

        public decimal TotalInvoiced { get; set; }

        public decimal TotalCollected { get; set; }

        public decimal OutstandingBalance { get; set; }

    }
}