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
            // N:M con payload: Talk (clave compuesta)
            // (Opcional) Índice único: Room.Name dentro de una Conference
            mb.Entity<Conference>()
                .HasKey(c => c.Id);
                .HasProperty(c => c.Title)
                .HasProperty(c => c.City)
                .HasProperty(c => c.StartDate)
                .HasProperty(c => c.EndDate)
                .HasMany(c => c.Rooms)
                .WithOne(r => r.Conference)
                .HasForeignKey(r => r.ConferenceId).Required()
                .OnDelete(DeleteBehavior.Cascade);
            mb.Entity<Room>()
                .HasKey(r => r.Id);
                .HasProperty(r => r.Name);
                .HasProperty(r => r.ConferenceId)
                .HasMany(r => r.Talks)
                .WithOne(t => t.Room)
                .HasForeignKey(t => t.StartTime)
                .HasForeignKey(t => t.EndTime);
            mb.Entity<Speaker>()
                .HasKey(s => s.Id);
                .HasProperty(s => s.FullName);
                .HasProperty(s => s.TopicArea);
                .HasMany(s => s.Talks)
                .WithOne(t => t.Speaker)
                .HasForeignKey(t => t.StartTime)
                .HasForeignKey(t => t.EndTime);
            mb.Entity<Talk>()
                .HasProperty(t => t.EndTime);
                .HasProperty(t => t.StartTime);
                .HasProperty(t => t.SpeakerId);
                .HasProperty(t => t.RoomId);

        }
    }
}
