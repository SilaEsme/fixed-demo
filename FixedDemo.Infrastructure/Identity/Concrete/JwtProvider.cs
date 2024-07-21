using FixedDemo.Application.Core.Abstract.Identity;
using FixedDemo.Infrastructure.Identity.Options;
using FixedDemo.Shared.Dtos.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FixedDemo.Infrastructure.Identity.Concrete
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public TokenDto GenerateToken(Domain.Entities.User user, double? validHours = null)
        {
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Iss, _jwtOptions.Iss),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };
            if (user.PhoneNumber != null) claims.Add(new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber));

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims, null, DateTime.Now.AddHours(validHours ?? _jwtOptions.ValidHours), signingCredentials);
            string tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
            return new TokenDto() { Token = tokenString, ExpireAt = token.ValidTo };

        }
    }
}
