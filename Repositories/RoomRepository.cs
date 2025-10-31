using _3ecexamen.Data;
using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace _3ecexamen.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _ctx;
        public RoomRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<bool> ExistsAsync(int id) =>
            _ctx.Rooms.AnyAsync(s => s.Id == id);
    }
}
