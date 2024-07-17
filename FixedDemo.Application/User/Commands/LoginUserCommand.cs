using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Application.Core.Abstract.Identity;
using FixedDemo.Application.Core.Dtos.User;
using FixedDemo.Application.Core.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FixedDemo.Application.User.Commands
{
    public record class LoginUserCommand : IRequest<Domain.Wrapper.ApiResult<UserDataDto>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Domain.Wrapper.ApiResult<UserDataDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public LoginUserCommandHandler(IDbContext context, IMapper mapper, IJwtProvider jwtProvider)
        {
            _context = context;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }
        public async Task<Domain.Wrapper.ApiResult<UserDataDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Set<Domain.Entities.User>().FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (user == null)
            {
                return Domain.Wrapper.ApiResult<UserDataDto>.Fail("User not found.", System.Net.HttpStatusCode.NotFound);
            }
            if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Domain.Wrapper.ApiResult<UserDataDto>.Fail("Invalid password.", System.Net.HttpStatusCode.BadRequest);
            }
            return Domain.Wrapper.ApiResult<UserDataDto>.Success(new UserDataDto() { User = _mapper.Map<UserDto>(user), Token = _jwtProvider.GenerateToken(user) }, System.Net.HttpStatusCode.OK);
        }
    }
}
