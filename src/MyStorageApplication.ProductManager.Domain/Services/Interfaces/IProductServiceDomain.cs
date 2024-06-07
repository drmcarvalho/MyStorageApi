﻿using MyStorageApplication.Database.Dtos;
using MyStorageApplication.ProductManager.Domain.Dtos;

namespace MyStorageApplication.ProductManager.Domain.Services.Interfaces
{
    public interface IProductServiceDomain
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ValidationResult> CreateAsync(CreateProductDto createProductDto);
        Task<ValidationResult> UpdateAsync(UpdateProductDto updateProductDto);
    }
}