using _3ecexamen.Data;
using Microsoft.EntityFrameworkCore;

namespace _3ecexamen.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _db;

        public RoomRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Rooms.AnyAsync(r => r.Id == id);
        }
    }
}
