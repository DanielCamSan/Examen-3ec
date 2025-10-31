using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using System.Runtime.CompilerServices;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        int conferenceId = 0;

        private readonly IConferenceRepository _confs;

        public ConferenceService(IConferenceRepository confs)
        {
            _confs = confs;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            conferenceId = conferenceId + 1;
            var conference = new Conference
            {
                Id = conferenceId,
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select(s => new Room { Name = s.Name }).ToList()
            };
            await _confs.SaveChangesAsync();
            return conference.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            return new ConferenceAgendaDto { Conference = conf.Title, City = conf.City, Rooms = conf.Rooms.Select
                (s=> new RoomScheduleDto { Room = s.Name, Talks = s.Talks.Select
                ( s=> new TalkDto { SpeakerId = s.SpeakerId, Speaker = s.Speaker.FullName, RoomId= s.RoomId, Room = s.Room.Name, EndTime= s.EndTime, StartTime= s.StartTime }).ToList()}).ToList()};
           
        }
    }
}