using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class TalkRepository : ITalkRepository
    {
        private readonly AppDbContext _ctx;
        public TalkRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Talk talk) => await _ctx.Talks.AddAsync(talk);

        // Overlap si: (start < existing.End) && (end > existing.Start)
        public Task<bool> HasOverlapAsync(int roomId, DateTime start, DateTime end) =>
            _ctx.Talks.AnyAsync(t => t.RoomId == roomId
                                  && start < t.EndTime
                                  && end > t.StartTime);

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
