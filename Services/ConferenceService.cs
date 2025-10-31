using _3ecexamen.Data;
using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository conf) => _confs = conf;

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            var entity = new Conference
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                City = dto.City,
                Rooms = dto.Rooms.Select(s => new Room { Name = s.Name }).ToList()
            }; 
            await _confs.AddAsync(entity);
            await _confs.SaveChangesAsync();
            return entity.Id;
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
                        RoomId = t.RoomId,
                        Room = t.Rooms.Name,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime
                    }).OrderBy(t => t.StartTime).ToList()
                }).ToList()
            };

        }
    }
}
