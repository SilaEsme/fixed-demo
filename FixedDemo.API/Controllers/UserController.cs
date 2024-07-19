using FixedDemo.API.Abstract;
using FixedDemo.Application.User.Commands;
using FixedDemo.Application.User.Queries;
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
        public async Task<IActionResult> Login([FromBody] LoginUserQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
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
