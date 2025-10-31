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
        //TODO  pista: usa speaker y talk service

        public SpeakersController(ISpeakerService speakers,ITalkService talks)
        {
            _speakers = speakers;
            _talks = talks;
        }
        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            return Ok(await _speakers.CreateAsync(dto));
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data= await _speakers.GetScheduleAsync(id);
            return data is null?NotFound():Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            await _talks.AddTalkAsync(dto);
            return Ok();
        }
    }
}
