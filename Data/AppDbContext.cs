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
            modelBuilder.Entity<Conference>(c =>
            {
                c.HasKey(c => c.Id);

            });
            modelBuilder.Entity<Room>(r =>
            {
                r.HasKey(r => r.Id);
                r.HasMany(t => t.Talks).WithOne(r => r.Room).HasForeignKey(r => r.RoomId);
                r.HasOne(r => r.Conference).WithMany(c => c.Rooms).HasForeignKey(r => r.ConferenceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Speaker>(s =>
            {
                s.HasKey(s => s.Id);
                s.HasMany(t => t.Talks).WithOne(s => s.Speaker).HasForeignKey(s=>s.SpeakerId);
            });

            modelBuilder.Entity<Talk>(t =>
            {
                t.HasKey(t => new {t.SpeakerId,t.RoomId});

            });
            // 1:N Conference -> Rooms (FK requerida, cascade)
            // N:M con payload: Talk (clave compuesta)
            // (Opcional) Índice único: Room.Name dentro de una Conference

        }
    }
}
