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

        private readonly ITalkService _talkService;
        private readonly ISpeakerService _speakerService;


        public SpeakersController(ISpeakerService service, ITalkService talkService)
        {
            _speakerService = service;
            _talkService = talkService;
        }
        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            //TODO
            var id = await _speakerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new {id =id}, new { id = id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data = await _speakerService.GetScheduleAsync(id);
            return data is null ? NotFound(new { error = "Speaker not found", status = 404 }) : Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            await _talkService.AddTalkAsync(dto);
            return Ok();
        }
    }
}
