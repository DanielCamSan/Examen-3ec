using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository conferencias)
        {
            _confs = conferencias;
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
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms
                    .OrderBy(r => r.Name).Select(r => new RoomScheduleDto
                    {
                        Room = r.Name
                    }).ToList()
            };
           
        }
    }
}
