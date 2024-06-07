using Microsoft.AspNetCore.Mvc;
using MyStorageApplication.Database.Dtos;
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
        [HttpGet("Product/Get/{id}")]
        [ProducesResponseType<StorageDto>(200)]
        public async Task<IActionResult> GetById([FromServices] IProductManagerServiceDomain service, int id)
        {
            var res = await service.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound(WithMessage(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Product")));
            }

            return Ok(res);
        }

        private object WithMessages(List<string> messages) => new { messages };

        private object WithMessage(string message) => new { message };
    }
}
