using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3ecexamen.Repositories
{
    public class TalkRepository : ITalkRepository
    {
        private readonly AppDbContext _db;

        public TalkRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Talk talk)
        {
            await _db.Talks.AddAsync(talk);
        }

        
        public async Task<bool> HasOverlapAsync(int roomId, DateTime start, DateTime end)
        {
            return await _db.Talks
                .AnyAsync(t =>
                    t.RoomId == roomId &&
                    t.StartTime < end &&
                    t.EndTime > start);
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
