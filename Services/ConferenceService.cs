using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;

        public ConferenceService(IConferenceRepository confs)
        {
            _confs = confs;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO DONE
            var confe = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,  
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name}).ToList()
            };
            await _confs.AddAsync(confe);
            await _confs.SaveChangesAsync();
            return confe.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            return new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms
                .OrderBy(r => r.Name)
                .Select(room => new RoomScheduleDto
                {
                    Room = room.Name,
                    Talks = room.Talks
                    .OrderBy(t => t.StartTime)
                    .Select(talkj => new TalkDto
                    {
                        SpeakerId = talkj.SpeakerId,
                        Speaker = talkj.Speaker.FullName,
                        RoomId = talkj.RoomId,
                        Room = room.Name,
                        StartTime = talkj.StartTime,
                        EndTime = talkj.EndTime
                        
                    }).ToList()
                }).ToList()
            };

        }
    }
}
