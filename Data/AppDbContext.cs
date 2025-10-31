using _3ecexamen.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            modelBuilder.Entity<Conference>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasMany(c => c.Rooms)
                 .WithOne(r => r.Conference)
                 .HasForeignKey(r => r.ConferenceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => new { x.ConferenceId, x.Name }).IsUnique();
            });
            modelBuilder.Entity<Speaker>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Talk>(e =>
            {
                e.HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });

                e.HasOne(t => t.Speaker)
                 .WithMany(s => s.Talks)
                 .HasForeignKey(t => t.SpeakerId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(t => t.Room)
                 .WithMany(r => r.Talks)
                 .HasForeignKey(t => t.RoomId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}