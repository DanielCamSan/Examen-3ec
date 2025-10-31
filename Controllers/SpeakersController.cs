using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerService _speakers;
        private readonly ITalkService _talks;

        public SpeakersController(ISpeakerService speakers, ITalkService talks)
        {
            _speakers = speakers;
            _talks = talks;
        }

     
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            var id = await _speakers.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

     
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data = await _speakers.GetScheduleAsync(id);
            if (data is null) return NotFound();
            return Ok(data);
        }

 
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            await _talks.AddTalkAsync(dto);
            return Ok();
        }
    }
}
