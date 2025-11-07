using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;
using System.Security.Claims;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceRegistrationController : ControllerBase
    {
        private readonly IRaceRegistrationService _registrationService;

        public RaceRegistrationController(IRaceRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Customer")]
        public IActionResult RegisterRace([FromBody] RaceRegistrationDto dto)
        {
            try
            {
                // Lấy UserId từ token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return Unauthorized("Không tìm thấy thông tin người dùng trong token.");

                int userId = int.Parse(userIdClaim);

                _registrationService.Register(userId, dto);
                return Ok(new { message = "Đăng ký giải đấu thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
