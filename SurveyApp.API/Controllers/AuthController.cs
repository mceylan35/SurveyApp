using MediatR;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Common.Interfaces.Services;
using SurveyApp.Application.DTOs.Requests;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IMediator mediator, IAuthService authService) : base(mediator)
        {
            
                _authService = authService;
             
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request.Email, request.Password);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return HandleResult(result);
        }
    }
}
