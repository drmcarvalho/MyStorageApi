using Microsoft.Extensions.DependencyInjection;
using MyStorageApplication.ProductManager.Domain.Services;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;

namespace MyStorageApplication.ProductManager.Domain
{
    public static class ContainerServiceProductManagerDomain
    {
        public static void AddContainerProductServiceDomain(this IServiceCollection services)
        {
            services.AddTransient<IProductManagerServiceDomain, ProductManagerServiceDomain>();
        }
    }
}
