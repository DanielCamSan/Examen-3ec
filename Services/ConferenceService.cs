using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;
        public ConferenceService(IConferenceRepository repo)
        {
            _confs = repo;
        }


        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            Conference conference = new()
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = dto.Rooms.Select((roomDto) => new Room()
                {
                    Name=roomDto.Name,
                }).ToList(),
            };

            await _confs.AddAsync(conference);
            await _confs.SaveChangesAsync();

            return conference.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto

            ConferenceAgendaDto agenda = new()
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms.Select((roomDto) => new RoomScheduleDto() {
                    Room=roomDto.Name,
                    Talks=roomDto.Talks.Select((talkDto)=>new TalkDto()
                    {
                        SpeakerId=talkDto.SpeakerId,
                        Speaker=talkDto.Speaker.FullName,
                        RoomId=talkDto.RoomId,
                        Room=talkDto.Room.Name,
                        StartTime=talkDto.StartTime,
                        EndTime=talkDto.EndTime,    

                    }).ToList(),
                }).ToList(),
            };
           
        }
    }
}
