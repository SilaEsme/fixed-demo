using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;

namespace FixedDemo.Application.Asset.Queries
{
    public record class GetAssetByIdQuery : IRequest<ApiResult<AssetDto>>
    {
        public required Guid Id { get; init; }
    }
}
