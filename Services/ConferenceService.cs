using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _confs;
        private readonly IRoomRepository _rooms;

        public ConferenceService(IConferenceRepository confs, IRoomRepository rooms)
        {
            _confs = confs;
            _rooms = rooms;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var conf = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name }).ToList()
            };

            await _confs.AddAsync(conf);
            await _confs.SaveChangesAsync();
            return conf.Id;
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
                Rooms = conf.Rooms.Select(r => new RoomScheduleDto
                {
                    Room = r.Name,
                    Talks = r.Talks.Select(t => new TalkDto
                    {
                        SpeakerId = t.SpeakerId,
                        Speaker = t.Speaker.FullName,
                        RoomId = r.Id,
                        Room = r.Name,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime
                    }).ToList()
                }).ToList()
            };
        }
    }
}

































