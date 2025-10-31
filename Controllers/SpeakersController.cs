using Microsoft.AspNetCore.Mvc;
using _3ecexamen.DTOs;
using _3ecexamen.Services;
using System.Threading.Tasks;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {
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
            var id = await _speakerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _speakerService.GetScheduleAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
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
