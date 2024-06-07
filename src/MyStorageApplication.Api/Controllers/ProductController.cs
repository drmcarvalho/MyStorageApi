using Microsoft.AspNetCore.Mvc;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.ProductManager.Domain.Dtos;
using MyStorageApplication.ProductManager.Domain.Helpers;
using MyStorageApplication.ProductManager.Domain.Services.Interfaces;
using MyStorageApplication.StorageManager.Domain.Dtos;
using MyStorageApplication.StorageManager.Domain.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MyStorageApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Gerenciamento de produtos")]
    public class ProductController : ControllerBase
    {
        [SwaggerOperation(Summary = "Busca um produto pelo id unico especificado")]
        [HttpGet("Product/Get/{id}")]
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
        [HttpGet("Product/GetAll")]
        [ProducesResponseType<IEnumerable<ProductDto>>(200)]
        public async Task<IActionResult> GetAll([FromServices] IProductManagerServiceDomain service)
            => Ok(await service.GetAllAsync());

        [SwaggerOperation(Summary = "Cadastrar um produto novo")]
        [HttpPost("Product/Create")]
        public async Task<IActionResult> CreateStorage([FromServices] IProductManagerServiceDomain service, [FromBody] CreateProductDto createProductDto)
        {
            var result = await service.CreateAsync(createProductDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Atualiza um produto cadastrado pelo id especificado")]
        [HttpPut("Product/Update")]
        public async Task<IActionResult> UpdateStorage([FromServices] IProductManagerServiceDomain service, [FromBody] UpdateProductDto updateProductDto)
        {
            var result = await service.UpdateAsync(updateProductDto);
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
