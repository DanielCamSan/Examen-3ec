using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferences;

        public ConferenceService(IConferenceRepository conferences)
        {
            _conferences = conferences;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var conf = new Conference
            {
                Title = dto.Title.Trim(),
                City = dto.City.Trim(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name.Trim() }).ToList()
            };

            await _conferences.AddAsync(conf);
            await _conferences.SaveChangesAsync();
            return conf.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _conferences.GetAgendaAsync(id);
            if (conf is null) return null;

           
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
                                SpeakerId = t.SpeakerId,
                                Speaker = t.Speaker.FullName,
                                RoomId = t.RoomId,
                                Room = r.Name,              
                                StartTime = t.StartTime,
                                EndTime = t.EndTime
                            }).ToList()
                    }).ToList()
            };
        }
       //esta coleccion es con la que va a calificar con todo lo que ya tengo deberia funcionar eso por que esa lista es la que me hizo importar a postman 
    }
}
