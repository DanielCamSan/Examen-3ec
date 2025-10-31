using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository confs) { 
            _confs = confs;
        
        }


        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {


            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms
            }; 
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            return null;
            return new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
            }; 



        }
    }
}
