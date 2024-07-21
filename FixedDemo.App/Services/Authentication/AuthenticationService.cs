using FixedDemo.Shared.Dtos.User;
using FixedDemo.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace FixedDemo.App.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> IsAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ApiResult<UserDataDto>> LoginAsync(LoginRequestDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/login", request);
            Console.WriteLine(result.Content);
            return await result.Content.ReadFromJsonAsync<ApiResult<UserDataDto>>();
        }

        public async Task<ApiResult<UserDataDto>> RegisterAsync(RegisterRequestDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/register", request);
            return await result.Content.ReadFromJsonAsync<ApiResult<UserDataDto>>();
        }
    }
}
