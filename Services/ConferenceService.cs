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
            //TODO
            var conf = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select(r => new Room
                {
                    Name = r.Name
                }).ToList()
            };


            return await _confs.AddAsync(conf);
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;

            var agenda = new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms.Select(room => new RoomScheduleDto
                {
                    Room = room.Name,
                    Talks = room.Talks
                        .Select(t => new TalkDto
                        {
                            SpeakerId = t.SpeakerId,
                            Speaker = t.Speaker.FullName,
                            RoomId = room.Id,
                            Room = room.Name,
                            StartTime = t.StartTime,
                            EndTime = t.EndTime
                        })
                        .OrderBy(t => t.StartTime)
                        .ToList()
                }).ToList()
            };

            return agenda;

        }
    }
}
