using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace _3ecexamen.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Conference> Conferences => Set<Conference>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Speaker> Speakers => Set<Speaker>();
        public DbSet<Talk> Talks => Set<Talk>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO

            // 1:N Conference -> Rooms (FK requerida, cascade)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Conference)
                .WithMany(c => c.Rooms)
                .HasForeignKey(r => r.ConferenceId)
                .OnDelete(DeleteBehavior.Cascade);

            // N:M con payload: Talk (clave compuesta)
            // (Opcional) Índice único: Room.Name dentro de una Conference

        }
    }
}
