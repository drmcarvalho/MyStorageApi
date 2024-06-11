using Microsoft.AspNetCore.Mvc;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.StorageManager.Domain.Dtos;
using MyStorageApplication.StorageManager.Domain.Helpers;
using MyStorageApplication.StorageManager.Domain.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MyStorageApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [Tags("Gerencimento de estoque de produtos")]
    [ApiController]
    public class StorageManagerController : ControllerBase
    {        
        [SwaggerOperation(Summary = "Busca um estoque pelo id unico especificado")]
        [HttpGet("Storage/Get/{id}")]
        [ProducesResponseType<StorageDto>(200)]
        public async Task<IActionResult> GetById([FromServices] IStorageManagerServiceDomain service, int id)
        {
            var res = await service.GetByIdAsync(id);
            if (res == null)
            {
                return NotFound(WithMessage(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Estoque")));
            }

            return Ok(res);
        }

        [SwaggerOperation(Summary = "Lista todos os estoques cadastrados")]
        [HttpGet("Storage/GetAll")]
        [ProducesResponseType<IEnumerable<StorageDto>>(200)]
        public async Task<IActionResult> GetAll([FromServices] IStorageManagerServiceDomain service) 
            => Ok(await service.GetAllAsync());

        [SwaggerOperation(Summary = "Pesquisa por um determinado estoque usando o campo identificação como critério")]
        [HttpGet("Storage/Query")]
        [ProducesResponseType<IEnumerable<StorageDto>>(200)]
        public async Task<IActionResult> StorageQuery([FromServices] IStorageManagerServiceDomain service, [FromQuery] string q)
            => Ok(await service.QueryStorage(q));

        [SwaggerOperation(Summary = "Cadastrar um estoque novo")]
        [HttpPost("Storage/Create")]
        public async Task<IActionResult> CreateStorage([FromServices] IStorageManagerServiceDomain service, [FromBody] CreateStorageDto createStorageDto)
        {
            var result = await service.CreateStorageAsync(createStorageDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Atualiza um estoque cadastrado pelo id especificado")]
        [HttpPut("Storage/Update")]
        public async Task<IActionResult> UpdateStorage([FromServices] IStorageManagerServiceDomain service, [FromBody] UpdateStorageDto updateStorageDto)
        {
            var result = await service.UpdateStorageAsync(updateStorageDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Registra uma nova movimentação no estoque")]        
        [HttpPost("Movement/RegisterMovementInStorage")]
        public async Task<IActionResult> RegisterMovementInStorage([FromServices] IStorageManagerServiceDomain service, [FromBody, SwaggerRequestBody(Description = "Payload com os dados da movimentação do estoque", Required = true)] RegisterMovementInStorageDto registerMovementInStorageDto)
        {
            var result = await service.RegisterMovementInStorage(registerMovementInStorageDto);
            if (!result.IsSuccess)
            {
                return BadRequest(WithMessages(result.ValidationMessageResult));
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Lista todas as movimentações em estoque em formato de histórico")]
        [HttpGet("Movement/GetAllHistoryMovements")]
        [ProducesResponseType<IEnumerable<HistoryMovementDto>>(200)]
        public async Task<IActionResult> GetAllHistoryMovements([FromServices] IStorageManagerServiceDomain service)
            => Ok(await service.GetAllHistoryMovimentsAsync());        

        private object WithMessages(List<string> messages) => new { messages };

        private object WithMessage(string  message) => new { message };
    }
}
