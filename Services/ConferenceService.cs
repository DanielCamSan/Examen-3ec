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

        public ConferenceService(IConferenceRepository confs) => _confs = confs;

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

            return new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms
                    .OrderBy(r => r.Name)
                    .Select(r => new RoomScheduleDto
                    {
                        Room = r.Name,
                        Talks = r.Talks
                            .OrderBy(t => t.StartTime)
                            .Select(t => new TalkDto
                            {
                                SpeakerId = t.Speaker.Id,
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
