using _3ecexamen.DTOs;

namespace _3ecexamen.Services
{
    public interface IConferenceService
    {
        Task<int> CreateConferenceAsync(CreateConferenceDto dto);
        Task<ConferenceAgendaDto?> GetAgendaAsync(int id);
    }
}
