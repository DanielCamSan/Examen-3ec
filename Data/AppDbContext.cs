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

           
            modelBuilder.Entity<Room>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);

                e.HasOne(x => x.Conference)
                 .WithMany(c => c.Rooms)
                 .HasForeignKey(x => x.ConferenceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

       
            modelBuilder.Entity<Talk>(e =>
            {
                e.HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });
                e.Property(t => t.StartTime).IsRequired();
                e.Property(t => t.EndTime).IsRequired();

                e.HasOne(t => t.Speaker)
                 .WithMany(s => s.Talks)
                 .HasForeignKey(t => t.SpeakerId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(t => t.Room)
                 .WithMany(r => r.Talks)
                 .HasForeignKey(t => t.RoomId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

       
            modelBuilder.Entity<Room>()
                        .HasIndex(r => new { r.ConferenceId, r.Name })
                        .IsUnique();
        }
    }
}
