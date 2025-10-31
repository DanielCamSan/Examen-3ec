using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;
        public  ConferenceService(ConferenceRepository conferes)
        {
            _confs = conferes;
        }
        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO

        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            return new ConferenceAgendaDto
            {
                Conference }0
            //TODO  pista: devuelve usando ConferenceAgendaDto
           
        }
    }
}
