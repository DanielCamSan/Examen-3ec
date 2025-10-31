using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            var entity = new Conference
            {
            
            };

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
