using _3ecexamen.DTOs;

namespace _3ecexamen.Services
{
    public interface ISpeakerService
    {
        Task<int> CreateAsync(CreateSpeakerDto dto);
        Task<SpeakerScheduleDto?> GetScheduleAsync(int id);
    }
}
