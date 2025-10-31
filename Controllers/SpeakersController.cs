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
        public readonly ISpeakerService _speakers;
        public readonly ITalkService _talks;

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
            if (!ModelState.IsValid) throw new ArgumentException();

            int res=await _speakers.CreateAsync(dto);
            return CreatedAtAction(nameof(Create), res);

        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var schedule = await _speakers.GetScheduleAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            //TODO
            if (!ModelState.IsValid) throw new ArgumentException();
            await _talks.AddTalkAsync(dto);
            return Created();
        }
    }
}
