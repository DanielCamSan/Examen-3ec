using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class TalkService : ITalkService
    {
        private readonly ITalkRepository _talks;
        private readonly ISpeakerRepository _speakers;
        private readonly IRoomRepository _rooms;

        public TalkService(ITalkRepository talks, ISpeakerRepository speakers, IRoomRepository rooms)
        {
            _talks = talks;
            _speakers = speakers;
            _rooms = rooms;
        }

        public async Task AddTalkAsync(CreateTalkDto dto)
        {
            
            if (!await _speakers.ExistsAsync(dto.SpeakerId))
                throw new KeyNotFoundException("Speaker not found");

            if (!await _rooms.ExistsAsync(dto.RoomId))
                throw new KeyNotFoundException("Room not found");

            
            if (dto.EndTime <= dto.StartTime)
                throw new InvalidOperationException("End time must be after start time");

            
            var overlaps = await _talks.HasOverlapAsync(dto.RoomId, dto.StartTime, dto.EndTime);
            if (overlaps)
                throw new InvalidOperationException("The room already has a talk in this time range.");

            
            var talk = new Talk
            {
                SpeakerId = dto.SpeakerId,
                RoomId = dto.RoomId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _talks.AddAsync(talk);
            await _talks.SaveChangesAsync();
        }
    }
}
