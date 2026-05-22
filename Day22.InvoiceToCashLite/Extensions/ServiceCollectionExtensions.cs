//gs
using Day22.InvoiceToCashLite.Data;
using Day22.InvoiceToCashLite.Features.Invoices;
using Day22.InvoiceToCashLite.Features.Invoices.Interfaces;

namespace Day22.InvoiceToCashLite.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddSingleton<InMemoryStore>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}