using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;

namespace FixedDemo.Application.Asset.Commands
{
    public record class UpdateAssetCommand : IRequest<ApiResult<AssetDto>>
    {
        public required Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
    internal sealed class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, ApiResult<AssetDto>>
    {
        public Task<ApiResult<AssetDto>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
