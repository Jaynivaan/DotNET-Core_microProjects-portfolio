//gs

using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Payments;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Data
{
    public class InMemoryStore
    {
        public List<Invoice> Invoices { get; } = new();

        public List<Payment> Payments { get; } = new();
    }
}
//temporary database setup..
