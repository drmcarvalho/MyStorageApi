using Microsoft.Extensions.DependencyInjection;
using MyStorageApplication.Database.Repositories;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database
{
    public static class ContainerDatabase
    {
        public static void AddContainerDatabase(this IServiceCollection services)
        {
            services.AddScoped<DatabaseSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Repositories
            services.AddTransient<IStorageReadOnlyRepository, StorageReadOnlyRepository>();
            services.AddTransient<IStorageWriteOnlyRepository, StorageWriteOnlyRepository>();
            services.AddTransient<IMovementsReadOnlyRepository, MovementsReadOnlyRepository>();
            services.AddTransient<IMovementsWriteOnlyRepository, MovementsWriteOnlyRepository>();
            services.AddTransient<IProductReadOnlyRepository, ProductReadOnlyRepository>();
            services.AddTransient<IProductWriteOnlyRepository, ProductWriteOnlyRepository>();
            services.AddTransient<IBalanceProductStorageReadOnlyRepository, BalanceProductStorageReadOnlyRepository>();
            services.AddTransient<IBalanceProductStorageWriteOnlyRepository, BalanceProductStorageWriteOnlyRepository>();
        }
    }
}
