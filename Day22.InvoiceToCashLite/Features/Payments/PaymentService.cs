//gs
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Payments.Interfaces;
using Day22.InvoiceToCashLite.Shared;
using System;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Features.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly InMemoryStore _store;

        public PaymentService(InMemoryStore store)
        { _store = store; }

        public ApiResponse<Payment> CreatePayment(CreatePaymentRequest request)
        {
            var invoice = _store.Invoices
                .FirstOrDefault(x => x.Id == request.InvoiceId);
            if (invoice == null)
            {
                return new ApiResponse<Payment>
                {
                    Success = false,
                    Message = "Invoice not found"
                };
            }

            if (request.Amount <= 0)
            {
                return new ApiResponse<Payment>
                {
                    Success = false,
                    Message = "Payment amount must be greater than zero."
                };
            }

            if (request.Amount > invoice.BalanceDue)
            {
                return new ApiResponse<Payment>
                {
                    Success = false,
                    Message = "Payment cannot exceed invoice Balance."
                };
            }

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                InvoiceId = request.InvoiceId,
                Amount = request.Amount,
                PaidAt = DateTime.UtcNow

            };
            _store.Payments.Add(payment);

            invoice.BalanceDue -= request.Amount;

            invoice.Status = invoice.BalanceDue == 0
                ? InvoiceStatus.Paid
                : InvoiceStatus.PartiallyPaid;

            return new ApiResponse<Payment>
            {
                Success = true,
                Message = "Payment applied successfully.",
                Data = payment
            };

        }

        public ApiResponse<List<Payment>>GetAllPayments()
        {
            return new ApiResponse<List<Payment>>
            {
                Success = true,
                Message = "Payment retrieved successfully",
                Data = _store.Payments

            };
        }
            
        
    }
}