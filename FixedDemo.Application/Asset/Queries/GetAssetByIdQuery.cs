using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FixedDemo.Application.Asset.Queries
{
    public record class GetAssetByIdQuery : IRequest<ApiResult<AssetDto>>
    {
        public required Guid Id { get; init; }
    }
    internal sealed class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, ApiResult<AssetDto>>
    {
        private readonly ILogger<GetAssetByIdQueryHandler> _logger;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAssetByIdQueryHandler(ILogger<GetAssetByIdQueryHandler> logger, IDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResult<AssetDto>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Asset? asset = await _dbContext.GetByIdAsync<Domain.Entities.Asset>(request.Id, cancellationToken);
            if (asset == null) return ApiResult<AssetDto>.Fail($"Asset not found.", System.Net.HttpStatusCode.NotFound);
            return ApiResult<AssetDto>.Success(_mapper.Map<AssetDto>(asset));
        }
    }
}
