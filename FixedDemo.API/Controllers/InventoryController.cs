using FixedDemo.API.Abstract;
using MediatR;
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new Application.Asset.Queries.GetAllAssetsQuery());
            return CreateActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new Application.Asset.Queries.GetAssetByIdQuery() { Id = id });
            return CreateActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Application.Asset.Commands.CreateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Application.Asset.Commands.UpdateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new Application.Asset.Commands.DeleteAssetCommand() { Id = id });
            return CreateActionResult(result);
        }
    }
}
