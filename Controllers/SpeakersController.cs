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
        private readonly ISpeakerService _speakerService;
        private readonly ITalkService _talkService;
        public SpeakersController(ISpeakerService speakerService, ITalkService talkService)
        {
            _speakerService = speakerService;
            _talkService = talkService;
        }
        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            var id= await _speakerService.CreateAsync(dto);
            return CreatedAtAction(nameof(Create), new {id}, new {id});
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data=await _speakerService.GetScheduleAsync(id);
            if (data==null) return NotFound();
            return Ok(data);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            var talk=_talkService.AddTalkAsync(dto);
            return CreatedAtAction(nameof(AddTalk));
        }
    }
}
