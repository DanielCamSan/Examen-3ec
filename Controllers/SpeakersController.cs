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
        private readonly ISpeakerService _service;
        private readonly ITalkService _talk;

        public SpeakersController(ISpeakerService service, ITalkService talk)
        {
            _service = service;
            _talk = talk;
        }


        // POST: api/v1/speakers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeakerDto dto)
        {
            var id=await _service.CreateAsync(dto);
            return Created($"api/v1/{id}", new { id });
        }

        // GET: api/v1/speakers/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data=await _service.GetScheduleAsync(id);
            if(data==null)
                return NotFound();
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
