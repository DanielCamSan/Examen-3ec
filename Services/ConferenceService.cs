using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _repo;
        public ConferenceService(IConferenceRepository repo)
        {
            _repo = repo;
        }


        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            Conference conference = new()
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select((roomDto) => new Room()
                {
                    Name=roomDto.Name,
                }).ToList(),
            };

            await _repo.AddAsync(conference);
            await _repo.SaveChangesAsync();

            return conference.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
           
        }
    }
}
