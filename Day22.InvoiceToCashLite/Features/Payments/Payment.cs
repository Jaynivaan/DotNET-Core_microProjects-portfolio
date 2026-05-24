//gs
using System;

namespace Day22.InvoiceToCashLite.Features.Payments
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid InvoiceId {  get; set; }

        public decimal Amount { get; set; }

        public DateTime PaidAt { get; set; }
    }
}