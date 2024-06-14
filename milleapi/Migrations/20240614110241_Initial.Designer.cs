﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using milleapi;

#nullable disable

namespace milleapi.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20240614110241_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("milleapi.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8195),
                            FirstName = "Jan",
                            IsDeleted = false,
                            LastName = "Kowalski"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8196),
                            FirstName = "Adam",
                            IsDeleted = false,
                            LastName = "Adamski"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8197),
                            FirstName = "Marcin",
                            IsDeleted = false,
                            LastName = "Nowak"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
