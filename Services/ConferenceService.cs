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
            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };


            if (dto.Rooms != null)
            {
                foreach (var r in dto.Rooms)
                {
                    entity.Rooms.Add(new Room { Name = r.Name });
                }
            }


            await _confs.AddAsync(entity);
            await _confs.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto

            var dto = new ConferenceAgendaDto
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
                SpeakerId = t.SpeakerId,
                Speaker = t.Speaker?.FullName ?? string.Empty,
                RoomId = t.RoomId,
                Room = r.Name,
                StartTime = t.StartTime,
                EndTime = t.EndTime
            }).ToList()
            }).ToList()
            };


            return dto;
        }
    }
}
