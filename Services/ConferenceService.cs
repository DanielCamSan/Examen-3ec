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
                StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc)
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
               Conference= conf.Conference,
                Slots= conf.Slots
                     .OrderBy(s => s.StartTime)
                     .Select(s => new TalkDto
                     {
                          SpeakerId = s.SpeakerId,
                          Speaker = s.Speaker,
                          RoomId = s.RoomId,
                          Room = s.Room,
                          StartTime = s.StartTime,
                          EndTime = s.EndTime
                     }).ToList()
           }
        }
    }
}
