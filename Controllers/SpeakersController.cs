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
        private readonly ISpeakerService _speaker;
        private readonly ITalkService _talk;

        public SpeakersController(ISpeakerService speaker, ITalkService talk)
        {
            _speaker = speaker;
            _talk = talk;
        }



        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO
            var id=await _speaker.CreateAsync(dto);
            return Created($"api/v1/speakers/{id}",new { id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var data = await _speaker.GetScheduleAsync(id);
            if(data ==null) return NotFound();
            return Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            //TODO
            await _talk.AddTalkAsync(dto);
            return Ok();
        }
    }
}
