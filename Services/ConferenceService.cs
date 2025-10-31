using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
       private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository confs)
        {
            _confs = confs;
        }
        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var conference = new Conference
            {
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Title = dto.Title,
                Rooms= dto.Rooms.Select(r=>new Room { Name = r.Name }).ToList(),
            };
            await _confs.AddAsync(conference);
            await _confs.SaveChangesAsync();
            return conference.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            return new ConferenceAgendaDto
            {
                City = conf.City,
                Conference = conf.Title,
                Rooms=conf.Rooms.Select(r=>new RoomScheduleDto { 
                Room=r.Name,
                Talks=r.Talks.Select(t=>new TalkDto { 
                EndTime=t.EndTime,
                StartTime=t.StartTime,
                RoomId=t.RoomId,
                Room=t.Room.Name,
                SpeakerId=t.SpeakerId,
                Speaker=t.Speaker.FullName
                }).ToList()
                }).ToList()
            };
            //TODO  pista: devuelve usando ConferenceAgendaDto
           
        }
    }
}
