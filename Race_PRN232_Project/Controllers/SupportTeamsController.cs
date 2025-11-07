using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupportTeamsController : ControllerBase
    {
        private readonly ISupportTeamService _supportTeamService;

        public SupportTeamsController(ISupportTeamService supportTeamService)
        {
            _supportTeamService = supportTeamService;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAll()
        {
            return Ok(_supportTeamService.GetAll());
        }

        [HttpPost("add-member")]
        [Authorize(Roles = "Admin,Support")]
        public IActionResult AddMember(int teamId, int userId, string role)
        {
            var ok = _supportTeamService.AddMember(teamId, userId, role);
            return ok ? Ok("Thêm thành viên thành công.") : BadRequest("Thành viên đã tồn tại.");
        }

        [HttpDelete("remove-member")]
        [Authorize(Roles = "Admin,Support")]
        public IActionResult RemoveMember(int teamId, int userId)
        {
            var ok = _supportTeamService.RemoveMember(teamId, userId);
            return ok ? Ok("Xóa thành viên thành công.") : NotFound("Không tìm thấy thành viên trong đội.");
        }
    }
}
