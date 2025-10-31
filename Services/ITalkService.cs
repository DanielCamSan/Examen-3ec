using _3ecexamen.DTOs;

namespace _3ecexamen.Services
{
    public interface ITalkService
    {
        Task AddTalkAsync(CreateTalkDto dto);
    }
}
