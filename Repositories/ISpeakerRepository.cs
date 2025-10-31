using _3ecexamen.Entities;

namespace _3ecexamen.Repositories
{
    public interface ISpeakerRepository
    {
        Task AddAsync(Speaker speaker);
        Task<Speaker?> GetScheduleAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
