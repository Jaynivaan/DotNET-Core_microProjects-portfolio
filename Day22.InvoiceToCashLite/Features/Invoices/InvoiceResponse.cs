//gs 
using System;

namespace Day22.InvoiceToCashLite.Features.Invoices
{
    public class InvoiceResponse
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal Amount { get; set; }

        public decimal BalanceDue { get; set; }

        public string Status { get; set; } = "";

    }
}