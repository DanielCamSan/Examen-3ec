using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3ecexamen.Repositories
{
    public class TalkRepository : ITalkRepository
    {
        private readonly AppDbContext _ctx;
        public TalkRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Talk talk) => await _ctx.Talks.AddAsync(talk);
        public Task<bool> HasOverlapAsync(int roomId, DateTime start, DateTime end) =>
            _ctx.Talks.AnyAsync(t => t.RoomId == roomId
                                  && start < t.EndTime
                                  && end > t.StartTime);

        public async Task<List<Talk>> GetBySpeakerAsync(int speakerId)
        {
            return await _ctx.Talks
                .Include(t => t.Speaker)
                .Include(t => t.Room)
                .Where(t => t.SpeakerId == speakerId)
                .ToListAsync();
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
