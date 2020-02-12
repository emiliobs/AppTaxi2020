﻿// <auto-generated />
using System;
using AppTaxi2020.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppTaxi2020.Web.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppTaxi2020.Web.Data.Entities.TaxiEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Plaque")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("Taxis");
                });

            modelBuilder.Entity("AppTaxi2020.Web.Data.Entities.TripDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int?>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripDetails");
                });

            modelBuilder.Entity("AppTaxi2020.Web.Data.Entities.TripEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Qualification")
                        .HasColumnType("real");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<double>("SourceLatitude")
                        .HasColumnType("float");

                    b.Property<double>("SourceLongitude")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<double>("TargetLatitude")
                        .HasColumnType("float");

                    b.Property<double>("TargetLongitude")
                        .HasColumnType("float");

                    b.Property<int?>("TaxiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaxiId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("AppTaxi2020.Web.Data.Entities.TripDetailEntity", b =>
                {
                    b.HasOne("AppTaxi2020.Web.Data.Entities.TripEntity", "Trip")
                        .WithMany("TripDetails")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("AppTaxi2020.Web.Data.Entities.TripEntity", b =>
                {
                    b.HasOne("AppTaxi2020.Web.Data.Entities.TaxiEntity", "Taxi")
                        .WithMany("Trips")
                        .HasForeignKey("TaxiId");
                });
#pragma warning restore 612, 618
        }
    }
}
