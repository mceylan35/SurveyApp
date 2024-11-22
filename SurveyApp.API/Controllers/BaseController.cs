using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Common.Results;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        protected IActionResult HandleCreatedResult<T>(Result<T> result)
        {
            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        protected IActionResult HandlePagedResult<T>(Result<T> result)
        {
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
