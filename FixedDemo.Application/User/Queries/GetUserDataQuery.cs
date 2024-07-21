using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Shared.Dtos.User;
using MediatR;
using System.Text.Json.Serialization;

namespace FixedDemo.Application.User.Queries
{
    public record class GetUserDataQuery : IRequest<Shared.Wrapper.ApiResult<UserDto>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
    internal sealed class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, Shared.Wrapper.ApiResult<UserDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public GetUserDataQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Shared.Wrapper.ApiResult<UserDto>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Set<Domain.Entities.User>().FindAsync([request.Id], cancellationToken: cancellationToken);
            if (user == null)
            {
                return Shared.Wrapper.ApiResult<UserDto>.Fail("User not found.", System.Net.HttpStatusCode.NotFound);
            }
            return Shared.Wrapper.ApiResult<UserDto>.Success(_mapper.Map<UserDto>(user), System.Net.HttpStatusCode.OK);
        }
    }
}
