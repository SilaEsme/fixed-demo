using FixedDemo.Domain.Primitives;
using FixedDemo.Domain.Wrapper;
using MediatR;

namespace FixedDemo.Application.Asset.Commands
{
    public record class DeleteAssetCommand : IRequest<ApiResult<NoContent>>
    {
        public required Guid Id { get; init; }
    }
    internal sealed class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, ApiResult<NoContent>>
    {
        public Task<ApiResult<NoContent>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
