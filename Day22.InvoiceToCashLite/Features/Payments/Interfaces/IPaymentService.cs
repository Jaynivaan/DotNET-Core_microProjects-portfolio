//gs

using Day22.InvoiceToCashLite.Shared;
using System.Collections.Generic;

namespace Day22.InvoiceToCashLite.Features.Payments.Interfaces
{
    public interface IPaymentService
    {
        ApiResponse<Payment> CreatePayment(CreatePaymentRequest request);

        ApiResponse<List<Payment>> GetAllPayments();
    }
}