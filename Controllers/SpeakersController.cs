using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _speakerService.CreateAsync(dto);

            // Devuelve 201 Created apuntando al schedule del speaker
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _speakerService.GetScheduleAsync(id);

            if (schedule == null)
                return NotFound(new { message = $"Speaker with ID {id} not found." });

            return Ok(schedule);
        }

        // POST: api/v1/speakers/talks
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _talkService.AddTalkAsync(dto);
                    return Ok(new { message = "Talk added successfully." });
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

        }
    }
}


