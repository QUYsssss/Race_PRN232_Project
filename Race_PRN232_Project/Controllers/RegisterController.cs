using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        [HttpPost("customer")]
        public IActionResult RegisterCustomer([FromBody] RegisterDTO dto)
        {
            //  Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.ConfirmPassword))
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin.");
            }

            //  Kiểm tra định dạng email
            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                return BadRequest("Email không hợp lệ.");
            }

            //  Kiểm tra độ dài mật khẩu
            if (dto.Password.Length < 6)
            {
                return BadRequest("Mật khẩu phải có ít nhất 6 ký tự.");
            }

            // Kiểm tra mật khẩu trùng khớp
            if (dto.Password != dto.ConfirmPassword)
            {
                return BadRequest("Mật khẩu xác nhận không khớp.");
            }

            //  Gán vai trò
            dto.Role = "Customer";

            //  Gọi service đăng ký
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
