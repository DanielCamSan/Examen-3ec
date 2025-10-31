using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _conferences;
        public ConferenceService(IConferenceRepository conferences)
        {
            _conferences = conferences;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate=DateTime.SpecifyKind(dto.StartDate,DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate,DateTimeKind.Utc),
                Rooms = dto.Rooms.Select(s=> new Room { Name=s.Name}).ToList(),
            };
            await _conferences.AddAsync(entity);
            await _conferences.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _conferences.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            return new ConferenceAgendaDto
            {
                Conference = conf.Title,//festival
                City = conf.City,
                Rooms= conf.Rooms //stages
                       .OrderBy(s=>s.Name)
                       .Select(s=>new RoomScheduleDto
                       {
                           Room = s.Name,
                           Talks = s.Talks //perfomance
                           .OrderBy(p => p.StartTime)
                           .Select(p => new TalkDto
                           {
                               SpeakerId = p.SpeakerId,
                               Speaker = p.Speaker.TopicArea,
                               RoomId = p.RoomId,
                               Room = s.Name,
                               StartTime = p.StartTime,
                               EndTime = p.EndTime,

                           }).ToList()
                       }).ToList()
              
     
            };
           
        }
    }
}
