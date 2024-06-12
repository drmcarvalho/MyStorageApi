using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.ProductManager.Domain.Dtos;
using MyStorageApplication.ProductManager.Domain.Helpers;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;

namespace MyStorageApplication.ProductManager.Domain.Services
{
    public class ProductManagerServiceDomain(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository, IBalanceProductStorageReadOnlyRepository balanceProductStorageReadOnlyRepository): ServiceValidationRule, IProductManagerServiceDomain
    {
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;
        private readonly IBalanceProductStorageReadOnlyRepository _balanceProductStorageReadOnlyRepository = balanceProductStorageReadOnlyRepository;

        public async Task<ValidationResult> CreateAsync(CreateProductDto createProductDto)
        {
            CheckRuleForEmptyField(createProductDto, nameof(createProductDto.Name));

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            await _productWriteOnlyRepository.InsertAsync(new Product(createProductDto.Name,  createProductDto.Price));

            return ValidationResult;
        }

        public async Task<ValidationResult> UpdateAsync(UpdateProductDto updateProductDto)
        {            
            var product = await _productReadOnlyRepository.GetByIdAsync(updateProductDto.ProductId);
            if (product is null)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Produto"));
            }

            CheckRuleForEmptyField(updateProductDto, nameof(updateProductDto.Name));

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            var productForUpdate = new Product();
            productForUpdate.Update(updateProductDto.ProductId, updateProductDto.Name, updateProductDto.Price);
            await _productWriteOnlyRepository.UpdateAsync(productForUpdate);
            
            return ValidationResult;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        { 
            var productsDto = await _productReadOnlyRepository.GetAllAsync(); 
            foreach (var product in productsDto)
            {
                var storages = await _balanceProductStorageReadOnlyRepository.GetBalanceStoragesByProduct(product.ProductId);
                if (storages.Any())
                {
                    product.Storages = string.Join(",", storages.ToList());
                }                
            }

            return productsDto;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
            => await _productReadOnlyRepository.GetByIdAsync(id);

        public async Task<ValidationResult> DeleteByIdAsync(int id)
        {
            var product = await _productReadOnlyRepository.GetByIdAsync(id);
            if (product is null)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Produto"));
            }

            await _productWriteOnlyRepository.DeleteAsync(id);

            return ValidationResult;
        }

        public async Task<IEnumerable<ProductDto>> QueryAsync(string query)
            => await _productReadOnlyRepository.QueryAsync(query);
    }
}
