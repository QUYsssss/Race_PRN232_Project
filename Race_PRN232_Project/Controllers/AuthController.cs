using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email và mật khẩu không được để trống.");

            var result = _authService.Authenticate(request.Email, request.Password);
            if (result == null)
                return Unauthorized("Sai email hoặc mật khẩu.");

            return Ok(result);
        }
    }
}
