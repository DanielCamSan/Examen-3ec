using _3ecexamen.Entities;

namespace _3ecexamen.Repositories
{
    public interface IConferenceRepository
    {
        Task AddAsync(Conference conf);
        Task<Conference?> GetAgendaAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
