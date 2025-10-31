 using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class TalkService : ITalkService
    {
        private readonly ITalkRepository _talks;
        private readonly IRoomRepository _rooms;
        private readonly ISpeakerRepository _speakers;

        public TalkService(ITalkRepository talks, IRoomRepository rooms, ISpeakerRepository speakers)
        {
            _talks = talks;
            _rooms = rooms;
            _speakers = speakers;
        }

        public async Task AddTalkAsync(CreateTalkDto dto)
        {
            var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
                : dto.StartTime.ToUniversalTime();
            var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
                : dto.EndTime.ToUniversalTime();

            if (endUtc <= startUtc)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            if (!await _speakers.ExistsAsync(dto.SpeakerId))
                throw new ArgumentException("Speaker not found.");

            if (!await _rooms.ExistsAsync(dto.RoomId))
                throw new ArgumentException("Room not found.");

            // BONUS: evitar solapamiento en la misma Room
            var overlaps = await _talks.HasOverlapAsync(dto.RoomId, startUtc, endUtc);
            if (overlaps)
                throw new InvalidOperationException("The room already has a talk in this time range.");

            var entity = new Talk
            {
                SpeakerId = dto.SpeakerId,
                RoomId = dto.RoomId,
                StartTime = startUtc,
                EndTime = endUtc
            };

            await _talks.AddAsync(entity);
            await _talks.SaveChangesAsync();
        }
    }
}
