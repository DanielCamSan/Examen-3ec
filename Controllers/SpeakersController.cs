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
        private readonly ISpeakerService _speak;
        private readonly ITalkService _talk;
        public SpeakersController(ISpeakerService speak, ITalkService talk)
        {
            _speak = speak;
            _talk = talk;
        }

        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO
            var id = await _speak.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var data = await _speak.GetScheduleAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            //TODO
            await _talk.AddTalkAsync(dto);
            if (_talk.AddTalkAsync(dto) == null) return NotFound();
            return Ok(dto);

        }
    }
}
