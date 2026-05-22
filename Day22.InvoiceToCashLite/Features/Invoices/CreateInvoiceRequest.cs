//gs

namespace Day22.InvoiceToCashLite.Features.Invoices
{
    public class CreateInvoiceRequest
    {
        public string CustomerName { get; set; } = "";

        public decimal Amount { get; set; }
    }
}