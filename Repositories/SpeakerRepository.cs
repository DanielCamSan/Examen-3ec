using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3ecexamen.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly AppDbContext _db;

        public SpeakerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Speaker speaker)
        {
            await _db.Speakers.AddAsync(speaker);
        }

        
        public async Task<Speaker?> GetScheduleAsync(int id)
        {
            return await _db.Speakers
                .Include(s => s.Talks)
                    .ThenInclude(t => t.Room)
                        .ThenInclude(r => r.Conference)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Speakers.AnyAsync(s => s.Id == id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
