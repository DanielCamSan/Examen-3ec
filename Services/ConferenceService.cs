using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using System.Linq;
using System.Threading.Tasks;


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
            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name }).ToList()
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
                Rooms = conf.Rooms
                    .OrderBy(r => r.Name)
                    .Select(room => new RoomScheduleDto
                    {
                        Room = room.Name,
                        Talks = room.Talks
                            .OrderBy(t => t.StartTime)
                            .Select(talk => new TalkDto
                            {
                                SpeakerId = talk.SpeakerId,
                                Speaker = talk.Speaker.FullName,
                                RoomId = talk.RoomId,
                                Room = room.Name,
                                StartTime = talk.StartTime,
                                EndTime = talk.EndTime
                            }).ToList()
                    }).ToList()
            };
        }
    }
}
