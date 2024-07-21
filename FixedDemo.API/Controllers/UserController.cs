using FixedDemo.API.Abstract;
using FixedDemo.Application.User.Commands;
using FixedDemo.Application.User.Queries;
using FixedDemo.Shared.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FixedDemo.API.Controllers
{
    public class UserController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _mediator.Send(new LoginUserQuery() { Email = request.Email, Password = request.Password });
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _mediator.Send(new RegisterUserCommand() 
            { 
                Email = request.Email, 
                Password = request.Password, 
                ConfirmPassword = request.ConfirmPassword, 
                Name = request.Name, 
                PhoneNumber = request.PhoneNumber 
            });
            return CreateActionResult(result);
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Me()
        {
            Guid userId = GetUserIdFromHeader();
            var result = await _mediator.Send(new GetUserDataQuery() { Id = userId });
            return CreateActionResult(result);
        }
    }
}
