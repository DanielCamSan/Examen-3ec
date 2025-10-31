using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly AppDbContext _ctx;
        public SpeakerRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();

        public async Task AddAsync(Speaker speaker) => await _ctx.Speakers.AddAsync(speaker);

        public Task<Speaker?> GetScheduleAsync(int id) =>
            _ctx.Speakers
                .Include(s => s.Talks)
                    .ThenInclude(t => t.Room)
                .FirstOrDefaultAsync(s => s.Id == id);

        public Task<bool> ExistsAsync(int id) =>
            _ctx.Speakers.AnyAsync(s => s.Id == id);
    }
}
