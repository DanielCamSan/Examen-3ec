using _3ecexamen.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _ctx;
        public RoomRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int id)
        {
            return await _ctx.Rooms.AnyAsync(r => r.Id == id);

        }
    }
}
