using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {
        //TODO  pista: usa speaker y talk service
        private readonly ISpeakerService _speakers;
        private readonly ITalkService _talks;
        public SpeakersController(ISpeakerService speakers, ITalkService talks)
        {
            _speakers = speakers;
            _talks = talks;
        }


        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO
            var id = await _speakers.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id = id }, null);
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var data = await _speakers.GetScheduleAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            //TODO
            await _talks.AddTalkAsync(dto);
            return Ok();
        }
    }
}