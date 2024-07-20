using FixedDemo.API.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixedDemo.API.Controllers
{
    public class InventoryController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAll(Application.Asset.Queries.GetAllAssetsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new Application.Asset.Queries.GetAssetByIdQuery() { Id = id });
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] Application.Asset.Commands.CreateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Application.Asset.Commands.UpdateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new Application.Asset.Commands.DeleteAssetCommand() { Id = id });
            return CreateActionResult(result);
        }
    }
}
