using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository _speakers;

        public SpeakerService(ISpeakerRepository speakers) => _speakers = speakers;

        public async Task<int> CreateAsync(CreateSpeakerDto dto)
        {
            var entity = new Speaker { FullName = dto.FullName, TopicArea = dto.TopicArea };
            await _speakers.AddAsync(entity);
            await _speakers.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<SpeakerScheduleDto?> GetScheduleAsync(int id)
        {
            var sp = await _speakers.GetScheduleAsync(id);
            if (sp == null) return null;

            return new SpeakerScheduleDto
            {
                Speaker = sp.FullName,
                Slots = sp.Talks
                    .OrderBy(t => t.StartTime)
                    .Select(t => new TalkDto
                    {
                        SpeakerId = sp.Id,
                        Speaker = sp.FullName,
                        RoomId = t.RoomId,
                        Room = t.Rooms.Name,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime
                    }).ToList()
            };
        }
    }
}
