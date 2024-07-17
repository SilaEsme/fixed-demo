using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FixedDemo.API.Abstract
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(Domain.Wrapper.ApiResult<T> result)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new NoContentResult();
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return new BadRequestObjectResult(result.Errors);
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return new UnauthorizedObjectResult(result.Errors);
            else if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                return new ForbidResult(result.Errors);
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new NotFoundObjectResult(result.Errors);
            else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                return new ObjectResult(result.Errors) { StatusCode = (int)result.StatusCode };
            else
                return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };
        }
        [NonAction]
        public Guid GetUserIdFromHeader()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();
            return Guid.Parse(userId);
        }
    }
}
