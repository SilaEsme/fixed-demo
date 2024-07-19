using FixedDemo.Application.Core.Dtos.User;

namespace FixedDemo.Application.Core.Abstract.Identity
{
    public interface IJwtProvider
    {
        public TokenDto GenerateToken(Domain.Entities.User user, double? validHours = null);
    }
}
