using Microsoft.Extensions.DependencyInjection;
using MyStorageApplication.StorageManager.Domain.Services;
using MyStorageApplication.StorageManager.Domain.Services.Interfaces;

namespace MyStorageApplication.StorageManager.Domain
{
    public static class ContainerServiceStorageManagerDomain
    {
        public static void AddContainerServiceStorageManagerDomain(this IServiceCollection services)
        {
            services.AddTransient<IStorageManagerServiceDomain, StorageManagerServiceDomain>();            
        }
    }
}
