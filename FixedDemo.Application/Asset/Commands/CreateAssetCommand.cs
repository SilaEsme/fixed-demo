using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Shared.Dtos.Asset;
using FixedDemo.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<CreateAssetCommandHandler> _logger;
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateAssetCommandHandler(ILogger<CreateAssetCommandHandler> logger, IDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<AssetDto>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Asset asset = _mapper.Map<Domain.Entities.Asset>(request);
            asset = _dbContext.Insert<Domain.Entities.Asset>(asset);
            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ApiResult<AssetDto>.Success(_mapper.Map<AssetDto>(asset));
        }
    }
}
