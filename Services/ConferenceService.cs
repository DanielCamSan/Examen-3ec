using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository confs) => _confs = confs; 

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var created = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await _confs.AddAsync(created);
            await _confs.SaveChangesAsync();
            return created.Id;
            
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            var rooms = conf.Rooms.Select(r => new RoomScheduleDto
            {
                Room = r.Name,
                Talks = r.Talks.Select(t => new TalkDto
                {
                    SpeakerId = t.SpeakerId,
                    Speaker = t.Speaker.FullName,
                    RoomId = t.RoomId,
                    Room = t.Room.Name,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime
                }).ToList()
            }).ToList();
            var agenda = new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = rooms
            };
            return agenda; 

           
        }
    }
}
