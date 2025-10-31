using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _ctx;
        public ConferenceRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Conference conf) => await _ctx.Conferences.AddAsync(conf);

        public Task<Conference?> GetAgendaAsync(int id) =>
            _ctx.Conferences
                .Include(c => c.Rooms)
                    .ThenInclude(r => r.Talks)
                        .ThenInclude(t => t.Speaker)
                .FirstOrDefaultAsync(c => c.Id == id);

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
