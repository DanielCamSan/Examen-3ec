using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _3ecexamen.Data;

#nullable disable

namespace _3ecexamen.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.10");

            modelBuilder.Entity("_3ecexamen.Entities.Conference", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("City")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<DateTime>("EndDate")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("StartDate")
                    .HasColumnType("TEXT");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Conferences");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Room", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("ConferenceId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("ConferenceId", "Name")
                    .IsUnique();

                b.ToTable("Rooms");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Speaker", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("FullName")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("TopicArea")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Speakers");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Talk", b =>
            {
                b.Property<int>("SpeakerId")
                    .HasColumnType("INTEGER");

                b.Property<int>("RoomId")
                    .HasColumnType("INTEGER");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("TEXT");

                b.HasKey("SpeakerId", "RoomId", "StartTime");

                b.HasIndex("RoomId");

                b.ToTable("Talks");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Room", b =>
            {
                b.HasOne("_3ecexamen.Entities.Conference", "Conference")
                    .WithMany("Rooms")
                    .HasForeignKey("ConferenceId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Conference");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Talk", b =>
            {
                b.HasOne("_3ecexamen.Entities.Room", "Room")
                    .WithMany("Talks")
                    .HasForeignKey("RoomId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("_3ecexamen.Entities.Speaker", "Speaker")
                    .WithMany("Talks")
                    .HasForeignKey("SpeakerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Room");

                b.Navigation("Speaker");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Conference", b =>
            {
                b.Navigation("Rooms");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Room", b =>
            {
                b.Navigation("Talks");
            });

            modelBuilder.Entity("_3ecexamen.Entities.Speaker", b =>
            {
                b.Navigation("Talks");
            });
#pragma warning restore 612, 618
        }
    }
}