using Day22.InvoiceToCashLite.Shared;
using System;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Features.Invoices.Interfaces
{
    public interface IInvoiceService
    {
        ApiResponse<InvoiceResponse> CreateInvoice(CreateInvoiceRequest request);

        ApiResponse<List<InvoiceResponse>> GetAllInvoices();

        ApiResponse<InvoiceResponse> GetInvoiceById(Guid id);

        ApiResponse<InvoiceResponse> CancelInvoice(Guid id);

    }
}
