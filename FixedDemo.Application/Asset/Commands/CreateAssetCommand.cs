using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;

namespace FixedDemo.Application.Asset.Commands
{
    public record class CreateAssetCommand : IRequest<ApiResult<AssetDto>>
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
    internal sealed class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ApiResult<AssetDto>>
    {
        public Task<ApiResult<AssetDto>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
