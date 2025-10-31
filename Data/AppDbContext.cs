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

            modelBuilder.Entity<Conference>(c => {
                c.HasKey(c => c.Id);
                c.Property(c => c.Title).IsRequired();
                c.Property(c => c.City).IsRequired();
                c.Property(c => c.StartDate).IsRequired();
                c.Property(c => c.EndDate).IsRequired();
                c.HasMany(c => c.Rooms).WithOne(r => r.Conference).HasForeignKey(r=> r.ConferenceId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(r => {
                r.HasKey(r => r.Id);
                r.Property(r => r.Name).IsRequired();
                r.HasMany(r => r.Talks).WithOne(t => t.Room).HasForeignKey(t => t.RoomId);
            });

            modelBuilder.Entity<Speaker>(s =>
            {
                s.HasKey(s => s.Id);
                s.Property(s => s.FullName).IsRequired();
                s.Property(s => s.TopicArea).IsRequired();
                s.HasMany(s => s.Talks).WithOne(t=>t.Speaker).HasForeignKey(t => t.SpeakerId);
            });

            modelBuilder.Entity<Talk>(x =>{

                x.Property(x => x.StartTime).IsRequired();
                x.Property(x => x.EndTime).IsRequired();
                x.HasKey(x => new { x.SpeakerId, x.RoomId, x.StartTime });

            });

        }
    }
}   
