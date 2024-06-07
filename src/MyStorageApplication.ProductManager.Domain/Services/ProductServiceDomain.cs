using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.ProductManager.Domain.Dtos;
using MyStorageApplication.ProductManager.Domain.Helpers;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;

namespace MyStorageApplication.ProductManager.Domain.Services
{
    public class ProductServiceDomain(IProductReadOnlyRepository productReadOnlyRepository, IProductWriteOnlyRepository productWriteOnlyRepository): ServiceValidationRule, IProductServiceDomain
    {
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;

        public async Task<ValidationResult> CreateAsync(CreateProductDto createProductDto)
        {
            CheckRuleForEmptyField(createProductDto, nameof(createProductDto.Name));

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            await _productWriteOnlyRepository.InsertAsync(new Product(createProductDto.Name, createProductDto.StockBalance, createProductDto.Price));

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
            => await _productReadOnlyRepository.GetAllAsync();

        public async Task<ProductDto?> GetByIdAsync(int id)
            => await _productReadOnlyRepository.GetByIdAsync(id);
    }
}
