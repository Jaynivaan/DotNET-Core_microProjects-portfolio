//gs

using Day22.InvoiceToCashLite.Features.Invoices;


namespace Day22.InvoiceToCashLite.Data
{
    public class InMemoryStore
    {
        public List<Invoice> Invoices { get; } = new();
    }
}
//temporary database setup..
