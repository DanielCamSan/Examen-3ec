using _3ecexamen.DTOs;
using _3ecexamen.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ecexamen.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceService _service;

        public ConferencesController(IConferenceService service) => _service = service;

        // POST: api/v1/conferences
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConferenceDto dto)
        {
            var id = await _service.CreateConferenceAsync(dto);
            return CreatedAtAction(nameof(GetAgenda), new { id }, new { id });
        }

        // GET: api/v1/conferences/{id}/agenda
        [HttpGet("{id:int}/agenda")]
        public async Task<IActionResult> GetAgenda([FromRoute] int id)
        {
            var data = await _service.GetAgendaAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }
    }
}
