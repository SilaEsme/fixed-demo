using FixedDemo.API.Abstract;
using FixedDemo.Shared.Dtos.Asset;
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
        public async Task<IActionResult> GetAll(GetInventoryRequestDto request)
        {
            var result = await _mediator.Send(new Application.Asset.Queries.GetAllAssetsQuery() { Filter = request.Filter});
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
        public async Task<IActionResult> Add([FromBody] CreateAssetRequestDto request)
        {
            var result = await _mediator.Send(new Application.Asset.Commands.CreateAssetCommand()
            {
                Brand = request.Brand,
                Model = request.Model,
                SerialNumber = request.SerialNumber,
            });
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAssetRequestDto request)
        {
            var result = await _mediator.Send(new Application.Asset.Commands.UpdateAssetCommand() 
            { 
                Id = request.Id,
                Brand = request.Brand,
                Model = request.Model,
                SerialNumber = request.SerialNumber,
            });
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
