﻿// <auto-generated />
using System;
using FlyMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlyMe.Migrations
{
    [DbContext(typeof(FlyMeContext))]
    partial class FlyMeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlyMe.Models.Airplane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<string>("Model");

                    b.HasKey("Id");

                    b.ToTable("Airplane");
                });

            modelBuilder.Entity("FlyMe.Models.Airport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("City");

                    b.Property<string>("Country");

                    b.HasKey("ID");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("FlyMe.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AirplaneId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DestAirportID");

                    b.Property<int?>("SourceAirportID");

                    b.HasKey("Id");

                    b.HasIndex("AirplaneId");

                    b.HasIndex("DestAirportID");

                    b.HasIndex("SourceAirportID");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("FlyMe.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuyerID");

                    b.Property<int>("FlightId");

                    b.Property<int>("LuggageWeight");

                    b.Property<int>("Price");

                    b.Property<string>("Seat");

                    b.HasKey("Id");

                    b.HasIndex("BuyerID");

                    b.HasIndex("FlightId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("FlyMe.Models.User", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsManager");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FlyMe.Models.Flight", b =>
                {
                    b.HasOne("FlyMe.Models.Airplane", "Airplane")
                        .WithMany("Flights")
                        .HasForeignKey("AirplaneId");

                    b.HasOne("FlyMe.Models.Airport", "DestAirport")
                        .WithMany()
                        .HasForeignKey("DestAirportID");

                    b.HasOne("FlyMe.Models.Airport", "SourceAirport")
                        .WithMany()
                        .HasForeignKey("SourceAirportID");
                });

            modelBuilder.Entity("FlyMe.Models.Ticket", b =>
                {
                    b.HasOne("FlyMe.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlyMe.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
