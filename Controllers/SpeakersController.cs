using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            var id = await _speakerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _speakerService.GetScheduleAsync(id);
            return schedule == null ? NotFound() : Ok(schedule);
        }
        [HttpPost("talks")]
        public async Task<IActionResult> AddTalk([FromBody] CreateTalkDto dto)
        {
            try
            {
                await _talkService.AddTalkAsync(dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}