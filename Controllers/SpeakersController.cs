using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpeakersController : ControllerBase
    {

        private readonly ISpeakerService _service;
        private readonly ITalkService _talkService;

        public SpeakersController(ISpeakerService service, ITalkService talkService)
        {
            _service = service;
            _talkService = talkService;
        }

        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {

            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id = id }, new { id = id });

        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {

            var data = await _service.GetScheduleAsync(id);
            if (data == null) return NotFound();
            return Ok(data);

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