using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;

namespace MyStorageApplication.ProductManager.Domain.Services
{
    public class ProductServiceDomain(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository): ServiceValidationRule, IProductServiceDomain
    {
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;


    }
}
