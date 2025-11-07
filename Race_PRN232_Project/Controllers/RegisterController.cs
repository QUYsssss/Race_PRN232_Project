using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthService _authService;

        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng ký tài khoản Runner (vai trò người tham gia cuộc đua)
        /// </summary>
        [HttpPost("runner")]
        public IActionResult RegisterRunner([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Role = "Runner";
            var success = _authService.Register(dto);

            if (!success)
                return BadRequest("Email đã tồn tại hoặc mật khẩu không hợp lệ.");

            return Ok("Đăng ký Runner thành công!");
        }

        /// <summary>
        /// Đăng ký tài khoản Collaborator (vai trò người hỗ trợ)
        /// </summary>
        [HttpPost("collaborator")]
        public IActionResult RegisterCollaborator([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Role = "Collaborator";
            var success = _authService.Register(dto);

            if (!success)
                return BadRequest("Email đã tồn tại hoặc mật khẩu không hợp lệ.");

            return Ok("Đăng ký Collaborator thành công!");
        }
    }
}
