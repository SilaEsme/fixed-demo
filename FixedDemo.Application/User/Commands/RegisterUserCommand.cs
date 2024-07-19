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
        public string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Domain.Wrapper.ApiResult<UserDataDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        public RegisterUserCommandHandler(IDbContext dbContext, IMapper mapper, IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Wrapper.ApiResult<UserDataDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(request);
            PasswordHasher.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var createdUser = _dbContext.Insert(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Domain.Wrapper.ApiResult<UserDataDto>.Success(new UserDataDto() { User = _mapper.Map<UserDto>(createdUser), Token = _jwtProvider.GenerateToken(createdUser) }, System.Net.HttpStatusCode.Created);
        }
    }
}
