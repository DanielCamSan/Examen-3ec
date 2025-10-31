using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            var Id = await _speaker.CreateAsync(dto);

            return Created($"api/v1/speakers/{Id}", new { Id });
        }
        public class CreateSpeakerDto
        {
            [Required] public string FullName { get; set; } = default!;
            [Required] public string TopicArea { get; set; } = default!;
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            //TODO
            var data = await _speaker.GetScheduleAsync(id);
            if (data == null) return NotFound();
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
