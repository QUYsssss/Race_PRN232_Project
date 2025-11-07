using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // yêu cầu đăng nhập
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [EnableQuery] // OData filter
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UserDTO dto)
        {
            var ok = _userService.Create(dto);
            return ok ? Ok("Thêm thành công") : BadRequest("Không thể thêm user.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, UserDTO dto)
        {
            dto.UserId = id;
            var ok = _userService.Update(dto);
            return ok ? Ok("Cập nhật thành công") : NotFound("User không tồn tại.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var ok = _userService.Delete(id);
            return ok ? Ok("Xóa thành công") : NotFound("User không tồn tại.");
        }
    }
}
