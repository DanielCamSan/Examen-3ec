using _3ecexamen.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _ctx;
        public RoomRepository(AppDbContext ctx) => _ctx = ctx;

        //TODO: HECHO
        public async Task<bool> ExistsAsync(int id)
        {
            var r = await _ctx.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (r == null) return false;
            return true;
        }
    }
}
