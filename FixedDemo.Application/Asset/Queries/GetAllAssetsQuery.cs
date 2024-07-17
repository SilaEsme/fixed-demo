using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;

namespace FixedDemo.Application.Asset.Queries
{
    public record class GetAllAssetsQuery : IRequest<ApiResult<List<AssetDto>>>
    {
    }
    internal sealed class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, ApiResult<List<AssetDto>>>
    {
        public Task<ApiResult<List<AssetDto>>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
