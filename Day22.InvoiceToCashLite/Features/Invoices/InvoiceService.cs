//gs
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Shared;
using Day22.InvoiceToCashLite.Features.Invoices.Interfaces;
using System;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Features.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InMemoryStore _store;

        public InvoiceService(InMemoryStore store)
        {
            _store = store;
        }

        public ApiResponse<InvoiceResponse> CreateInvoice(CreateInvoiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return new ApiResponse<InvoiceResponse>
                {
                    Success = false,
                    Message = "Customer name is Required!."
                };
            }

            if (request.Amount <= 0)
            {
                return new ApiResponse<InvoiceResponse>
                {
                    Success = false,
                    Message = "Invoice amount must be greater than zero.."
                };
            }

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                Amount = request.Amount,
                BalanceDue = request.Amount,
                Status = InvoiceStatus.Open
            };
            _store.Invoices.Add(invoice);

            return new ApiResponse<InvoiceResponse>
            {
                Success = true,
                Message = "Invoice created successfully.",
                Data = MapToResponse(invoice)
            };
        }

        public ApiResponse<List<InvoiceResponse>> GetAllInvoices()
        {
            var invoice = _store.Invoices
                .Select(MapToResponse)
                .ToList();

            return new ApiResponse<List<InvoiceResponse>>
            {
                Success = true,
                Message = "Invoices retrieved Successfully.",
                Data = invoice
            };
        }

        public ApiResponse<InvoiceResponse>GetInvoiceById(Guid Id)
        {
            var invoice = _store.Invoices.FirstOrDefault(x => x.Id == Id);

            if (invoice == null)
            {
                return new ApiResponse<InvoiceResponse>
                {
                    Success = false,
                    Message = "Invoice not found"
                };
            }

            return new ApiResponse<InvoiceResponse>
            {
                Success = true,
                Message = "Invoice found",
                Data = MapToResponse(invoice)
            };
        }
        
        private static InvoiceResponse MapToResponse(Invoice invoice)
        {
            return new InvoiceResponse
            {
                Id = invoice.Id,
                CustomerName = invoice.CustomerName,
                Amount = invoice.Amount,
                BalanceDue = invoice.BalanceDue,
                Status = invoice.Status.ToString()
            };
        }
    }
}