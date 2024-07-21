using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Domain.Primitives;
using FixedDemo.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FixedDemo.Application.Asset.Commands
{
    public record class DeleteAssetCommand : IRequest<ApiResult<NoContent>>
    {
        public required Guid Id { get; init; }
    }
    internal sealed class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, ApiResult<NoContent>>
    {
        private readonly ILogger<DeleteAssetCommandHandler> _logger;
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAssetCommandHandler(ILogger<DeleteAssetCommandHandler> logger, IDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<NoContent>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Remove<Domain.Entities.Asset>(request.Id);
            _ = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ApiResult<NoContent>.Success();
        }
    }
}
