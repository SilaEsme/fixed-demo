using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Application.Core.Dtos.Asset;
using FixedDemo.Domain.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<UpdateAssetCommandHandler> _logger;
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAssetCommandHandler(ILogger<UpdateAssetCommandHandler> logger, IDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<AssetDto>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Asset? asset = await _dbContext.GetByIdAsync<Domain.Entities.Asset>(request.Id, cancellationToken);
            if (asset == null) return ApiResult<AssetDto>.Fail("Asset not found.", System.Net.HttpStatusCode.NotFound);

            Domain.Entities.Asset updatedAsset = _mapper.Map(request, asset);
            _dbContext.Set<Domain.Entities.Asset>().Update(updatedAsset);
            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ApiResult<AssetDto>.Success(_mapper.Map<AssetDto>(updatedAsset));
        }
    }
}
