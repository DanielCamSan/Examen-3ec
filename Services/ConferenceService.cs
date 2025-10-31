using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferences;

        public ConferenceService(IConferenceRepository conferences) => _conferences = conferences;

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var entity = new Conference { Title = dto.Title, City = dto.City, StartDate = dto.StartDate, EndDate = dto.EndDate };
            await _conferences.AddAsync(entity);
            await _conferences.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _conferences.GetAgendaAsync(id);
            if (conf == null) return null;

            return new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms
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
                            })
                            .ToList()
                    })
                    .ToList()
            };
        }

    }
}
