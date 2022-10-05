﻿// <auto-generated />
using System;
using CarRentalSystem.Dealers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalSystem.Dealers.Data.Migrations
{
    [DbContext(typeof(DealersDbContext))]
    partial class DealersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("CarAd")
                .IncrementsBy(10);

            modelBuilder.Entity("CarRentalSystem.Data.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serializedData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.CarAd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "CarAd");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId1")
                        .HasColumnType("int");

                    b.Property<int>("DealerId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<int?>("ManufacturerId1")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CategoryId1");

                    b.HasIndex("DealerId");

                    b.HasIndex("IsAvailable");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ManufacturerId1");

                    b.ToTable("CarAds");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Dealer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dealers");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.CarAd", b =>
                {
                    b.HasOne("CarRentalSystem.Dealers.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarRentalSystem.Dealers.Data.Models.Category", null)
                        .WithMany("CarAds")
                        .HasForeignKey("CategoryId1");

                    b.HasOne("CarRentalSystem.Dealers.Data.Models.Dealer", "Dealer")
                        .WithMany("CarAds")
                        .HasForeignKey("DealerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarRentalSystem.Dealers.Data.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarRentalSystem.Dealers.Data.Models.Manufacturer", null)
                        .WithMany("CarAds")
                        .HasForeignKey("ManufacturerId1");

                    b.OwnsOne("CarRentalSystem.Dealers.Data.Models.Options", "Options", b1 =>
                        {
                            b1.Property<int>("CarAdId")
                                .HasColumnType("int");

                            b1.Property<bool>("HasClimateControl")
                                .HasColumnType("bit");

                            b1.Property<int>("NumberOfSeats")
                                .HasColumnType("int");

                            b1.Property<int>("TransmissionType")
                                .HasColumnType("int");

                            b1.HasKey("CarAdId");

                            b1.ToTable("CarAds");

                            b1.WithOwner()
                                .HasForeignKey("CarAdId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Dealer");

                    b.Navigation("Manufacturer");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Category", b =>
                {
                    b.Navigation("CarAds");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Dealer", b =>
                {
                    b.Navigation("CarAds");
                });

            modelBuilder.Entity("CarRentalSystem.Dealers.Data.Models.Manufacturer", b =>
                {
                    b.Navigation("CarAds");
                });
#pragma warning restore 612, 618
        }
    }
}
