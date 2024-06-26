﻿using Microsoft.AspNetCore.Mvc;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.ProductManager.Domain.Dtos;
using MyStorageApplication.ProductManager.Domain.Helpers;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MyStorageApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Gerenciamento de produtos")]
    public class ProductController : ControllerBase
    {
        [SwaggerOperation(Summary = "Busca um produto pelo id unico especificado")]
        [HttpGet("Get/{id}")]
        [ProducesResponseType<ProductDto>(200)]
        public async Task<IActionResult> GetById([FromServices] IProductManagerServiceDomain service, int id)
        {
            var res = await service.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound(WithMessage(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Produto")));
            }

            return Ok(res);
        }

        [SwaggerOperation(Summary = "Lista todos os produtos cadastrados")]
        [HttpGet("GetAll")]
        [ProducesResponseType<IEnumerable<ProductDto>>(200)]
        public async Task<IActionResult> GetAll([FromServices] IProductManagerServiceDomain service)
            => Ok(await service.GetAllAsync());

        [SwaggerOperation(Summary = "Pesquisar um produto pela descrição")]
        [HttpGet("Query")]
        [ProducesResponseType<IEnumerable<ProductDto>>(200)]
        public async Task<IActionResult> Query([FromServices] IProductManagerServiceDomain service, [FromQuery] string q)
            => Ok(await service.QueryAsync(q));

        [SwaggerOperation(Summary = "Cadastrar um produto novo")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromServices] IProductManagerServiceDomain service, [FromBody] CreateProductDto createProductDto)
        {
            var result = await service.CreateAsync(createProductDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Atualiza um produto cadastrado pelo id especificado")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromServices] IProductManagerServiceDomain service, [FromBody] UpdateProductDto updateProductDto)
        {
            var result = await service.UpdateAsync(updateProductDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Exclui um produto cadastrado pelo id especificado")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct([FromServices] IProductManagerServiceDomain service, int id)
        {
            var result = await service.DeleteByIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        private object WithMessages(List<string> messages) => new { messages };

        private object WithMessage(string message) => new { message };
    }
}
