//gs

using System;

namespace Day22.InvoiceToCashLite.Features.Payments
{
    public class CreatePaymentRequest
    {
        public Guid InvoiceId { get; set; }

        public decimal Amount { get; set; }
    }
}