using FixedDemo.Application.Core.Abstract.Identity;
using FixedDemo.Application.Core.Dtos.User;

namespace FixedDemo.Infrastructure.Identity.Concrete
{
    internal sealed class JwtProvider : IJwtProvider
    {
        public TokenDto GenerateToken(Domain.Entities.User user)
        {
            throw new NotImplementedException();
        }
    }
}
