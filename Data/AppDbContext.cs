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
            modelBuilder.Entity<Conference>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(100);
                entity.Property(c => c.City).IsRequired().HasMaxLength(100);
                entity.Property(c => c.StartDate).IsRequired();
                entity.Property(c => c.EndDate).IsRequired();

                entity.HasMany(c => c.Rooms)
                      .WithOne(r => r.Conference)
                      .HasForeignKey(r => r.ConferenceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // N:M con payload: Talk (clave compuesta)
            modelBuilder.Entity<Talk>(entity =>
            {
                entity.HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });

                // Relaciones explícitas (Speaker <-> Talk, Room <-> Talk)
                entity.HasOne(t => t.Speaker)
                      .WithMany(s => s.Talks)
                      .HasForeignKey(t => t.SpeakerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Room)
                      .WithMany(r => r.Talks)
                      .HasForeignKey(t => t.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // (Opcional) Índice único: Room.Name dentro de una Conference
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.ConferenceId, r.Name })
                .IsUnique();


        }
    }
}
