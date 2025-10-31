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
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Conference> (b =>
            {
                b.ToTable("Conferences");
                b.HasKey(c => c.Id);
                b.Property(c => c.Title).IsRequired();
                b.Property(c => c.City).IsRequired();
                b.Property(c => c.StartDate).IsRequired();
                b.Property(c => c.EndDate).IsRequired();
            });

            modelBuilder.Entity<Room>(b =>
            {
                b.ToTable("Rooms");
                b.HasKey(r => r.Id);
                b.Property(r => r.Name).IsRequired();
                b.Property(r => r.ConferenceId).IsRequired();
                                
                b.HasIndex(r => new { r.ConferenceId, r.Name }).IsUnique();
                                
                b.HasOne(r => r.Conference)
                 .WithMany(c => c.Rooms)
                 .HasForeignKey(r => r.ConferenceId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<Speaker>(b =>
            {
                b.ToTable("Speakers");
                b.HasKey(s => s.Id);
                b.Property(s => s.FullName).IsRequired();
                b.Property(s => s.TopicArea).IsRequired();
            });

            modelBuilder.Entity<Talk>(b =>
            {
                b.ToTable("Talks");

                b.HasKey(t => new { t.SpeakerId, t.RoomId, t.StartTime });

                b.Property(t => t.StartTime).IsRequired();
                b.Property(t => t.EndTime).IsRequired();

                b.HasOne(t => t.Speaker)
                 .WithMany(s => s.Talks)
                 .HasForeignKey(t => t.SpeakerId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(t => t.Room)
                 .WithMany(r => r.Talks)
                 .HasForeignKey(t => t.RoomId)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasIndex(t => t.RoomId);
            });

        }
    }
}