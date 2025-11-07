using Microsoft.AspNetCore.Mvc;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTeamMemberController : ControllerBase
    {
        private readonly ISupportTeamMemberService _service;

        public SupportTeamMemberController(ISupportTeamMemberService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("team/{teamId}")]
        public IActionResult GetByTeam(int teamId) => Ok(_service.GetByTeamId(teamId));

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSupportTeamMemberDTO dto)
        {
            var result = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.SupportTeamMemberId }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSupportTeamMemberDTO dto)
        {
            if (!_service.Update(id, dto)) return NotFound();
            return Ok(new { message = "Cập nhật thành công!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_service.Delete(id)) return NotFound();
                return Ok(new { message = "Xóa thành công!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
