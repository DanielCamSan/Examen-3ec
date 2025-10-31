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
            base.OnModelCreating(modelBuilder);
            //TODO

            // 1:N Conference -> Rooms (FK requerida, cascade)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Conference).WithMany(f => f.Rooms).HasForeignKey(r => r.ConferenceId).OnDelete(DeleteBehavior.Cascade);
            // N:M con payload: Talk (clave compuesta)
            modelBuilder.Entity<Talk>().HasKey(p => new { p.SpeakerId, p.RoomId, p.StartTime });

            // (Opcional) Índice único: Room.Name dentro de una Conference
            modelBuilder.Entity<Room>().HasIndex(s => new { s.ConferenceId, s.Name }).IsUnique();

        }
    }
}
