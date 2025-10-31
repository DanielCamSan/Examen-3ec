using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3ecexamen.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _db;

        public ConferenceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Conference conf)
        {
            await _db.Conferences.AddAsync(conf);
        }

        public async Task<Conference?> GetAgendaAsync(int id)
        {
            return await _db.Conferences
                .Include(c => c.Rooms)
                    .ThenInclude(r => r.Talks)
                        .ThenInclude(t => t.Speaker)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
