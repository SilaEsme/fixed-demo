using AutoMapper;
using FixedDemo.Application.Core.Abstract.Data;
using FixedDemo.Application.Core.Abstract.Identity;
using FixedDemo.Application.Core.Dtos.User;
using FixedDemo.Application.Core.Helpers;
using MediatR;

namespace FixedDemo.Application.User.Commands
{
    public record class RegisterUserCommand : IRequest<Domain.Wrapper.ApiResult<UserDataDto>>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Domain.Wrapper.ApiResult<UserDataDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public RegisterUserCommandHandler(IDbContext dbContext, IMapper mapper, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }
        public async Task<Domain.Wrapper.ApiResult<UserDataDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(request);
            PasswordHasher.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            var createdUser = _dbContext.Insert(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Domain.Wrapper.ApiResult<UserDataDto>.Success(new UserDataDto() { User = _mapper.Map<UserDto>(createdUser), Token = _jwtProvider.GenerateToken(createdUser) }, System.Net.HttpStatusCode.Created);
        }
    }
}
