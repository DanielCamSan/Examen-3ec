using _3ecexamen.Entities;

namespace _3ecexamen.Repositories
{
    public interface ITalkRepository
    {
        Task AddAsync(Talk talk);
        Task<bool> HasOverlapAsync(int roomId, DateTime start, DateTime end);
        Task<int> SaveChangesAsync();
    }
}
