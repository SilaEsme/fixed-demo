using FixedDemo.Shared.Dtos.User;
using FixedDemo.Shared.Wrapper;

namespace FixedDemo.App.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResult<UserDataDto>> LoginAsync(LoginRequestDto request);
        Task<ApiResult<UserDataDto>> RegisterAsync(RegisterRequestDto request);
        Task<bool> IsAuthenticated();
    }
}
