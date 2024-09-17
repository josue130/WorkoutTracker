using Microsoft.AspNetCore.Mvc;
using WorkoutApi.Models.Dto;
using WorkoutApi.Service.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkoutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null) 
            {
                _response.IsSucces = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSucces = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok();
        }


    }
}
