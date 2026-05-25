//gs
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Invoices.Interfaces;
using Day22.InvoiceToCashLite.Features.Payments;
using Day22.InvoiceToCashLite.Features.Payments.Interfaces;
using Day22.InvoiceToCashLite.Features.Reconciliation;
using Day22.InvoiceToCashLite.Features.Reconciliation.Interfaces;
using Day22.InvoiceToCashLite.Features.Dashboard;
using Day22.InvoiceToCashLite.Features.Dashboard.Interfaces;


namespace Day22.InvoiceToCashLite.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddSingleton<InMemoryStore>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReconciliationService, ReconciliationService>();
            services.AddScoped<IDashboardService, DashboardService>();
            return services;
        }
    }
}