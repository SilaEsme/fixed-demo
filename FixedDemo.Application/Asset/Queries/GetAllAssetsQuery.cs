using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Application.Core.Extentions;
using FixedDemo.Shared.Dtos;
using FixedDemo.Shared.Dtos.Asset;
using FixedDemo.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FixedDemo.Application.Asset.Queries
{
    public record class GetAllAssetsQuery : IRequest<ApiResult<IEnumerable<AssetDto>>>
    {
        public List<FilterDto>? Filter { get; set; }
    }
    internal sealed class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, ApiResult<IEnumerable<AssetDto>>>
    {
        private readonly ILogger<GetAllAssetsQueryHandler> _logger;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllAssetsQueryHandler(ILogger<GetAllAssetsQueryHandler> logger, IDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Task<ApiResult<IEnumerable<AssetDto>>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Asset> query = _dbContext.Set<Domain.Entities.Asset>().Where(x => !x.IsDeleted);

            if (request.Filter != null)
            {
                foreach (var filter in request.Filter)
                {
                    query = query.Where(filter.ToExpression<Domain.Entities.Asset>());
                }
            }
            var assets = query.ToList().Select(_mapper.Map<AssetDto>);
            return Task.FromResult(ApiResult<IEnumerable<AssetDto>>.Success(assets));
        }
    }
}
