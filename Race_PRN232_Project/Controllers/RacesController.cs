using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAll()
        {
            return Ok(_raceService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var race = _raceService.GetById(id);
            if (race == null) return NotFound();
            return Ok(race);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(RaceDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ok = _raceService.Create(dto);
            return ok ? Ok("Tạo cuộc thi thành công.") : BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, RaceDTO dto)
        {
            dto.RaceId = id;
            var ok = _raceService.Update(dto);
            return ok ? Ok("Cập nhật thành công.") : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var ok = _raceService.Delete(id);
            return ok ? Ok("Xóa thành công.") : NotFound();
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRace([FromBody] RaceCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ok = _raceService.CreateRace(dto);
            return ok ? Ok("Tạo giải chạy mới thành công!") : BadRequest("Không thể tạo giải.");
        }

    }
}
