using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs.RoleDTO;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

   
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

      
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var role = _roleService.GetById(id);
            if (role == null) return NotFound("Role not found.");
            return Ok(role);
        }

     
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CreateRoleDTO dto)
        {
            var createdRole = _roleService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdRole.RoleId }, createdRole);
        }

   
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] UpdateRoleDTO dto)
        {
            var result = _roleService.Update(id, dto);
            if (!result) return NotFound("Role not found.");
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _roleService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy vai trò." });

                return Ok(new { message = "Xóa vai trò thành công." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
