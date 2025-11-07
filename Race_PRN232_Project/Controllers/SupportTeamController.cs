using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs.SupportTeamDTO;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SupportTeamController : ControllerBase
    {
        private readonly ISupportTeamService _supportTeamService;

        public SupportTeamController(ISupportTeamService supportTeamService)
        {
            _supportTeamService = supportTeamService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll() => Ok(_supportTeamService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var team = _supportTeamService.GetById(id);
            if (team == null) return NotFound(new { message = "Không tìm thấy đội hỗ trợ." });
            return Ok(team);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSupportTeamDTO dto)
        {
            var created = _supportTeamService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SupportTeamId }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSupportTeamDTO dto)
        {
            bool updated = _supportTeamService.Update(id, dto);
            if (!updated) return NotFound(new { message = "Không tìm thấy đội hỗ trợ." });
            return Ok(new { message = "Cập nhật thành công." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _supportTeamService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy đội hỗ trợ." });

                return Ok(new { message = "Xóa đội hỗ trợ thành công." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
