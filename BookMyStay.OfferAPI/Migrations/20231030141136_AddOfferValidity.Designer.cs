﻿// <auto-generated />
using System;
using BookMyStay.OfferAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookMyStay.OfferAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231030141136_AddOfferValidity")]
    partial class AddOfferValidity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.7.23375.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookMyStay.OfferAPI.Models.Offer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferId"));

                    b.Property<string>("OfferCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfferDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OfferDiscountPerc")
                        .HasColumnType("float");

                    b.Property<DateTime>("OfferValidTill")
                        .HasColumnType("datetime2");

                    b.HasKey("OfferId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            OfferId = 1,
                            OfferCode = "FLAT05P",
                            OfferDetail = "Get flat 5 percent discount on your stay.",
                            OfferDiscountPerc = 5.0,
                            OfferValidTill = new DateTime(2023, 11, 24, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3796)
                        },
                        new
                        {
                            OfferId = 2,
                            OfferCode = "FLAT10P",
                            OfferDetail = "Get flat 10 percent discount on your stay.",
                            OfferDiscountPerc = 10.0,
                            OfferValidTill = new DateTime(2023, 11, 19, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3861)
                        },
                        new
                        {
                            OfferId = 3,
                            OfferCode = "FLAT15P",
                            OfferDetail = "Get flat 15 percent discount on your stay.",
                            OfferDiscountPerc = 15.0,
                            OfferValidTill = new DateTime(2023, 11, 14, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3875)
                        },
                        new
                        {
                            OfferId = 4,
                            OfferCode = "FLAT20P",
                            OfferDetail = "Get flat 20 percent discount on your stay.",
                            OfferDiscountPerc = 20.0,
                            OfferValidTill = new DateTime(2023, 11, 9, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3888)
                        },
                        new
                        {
                            OfferId = 5,
                            OfferCode = "FLAT25P",
                            OfferDetail = "Get flat 25 percent discount on your stay.",
                            OfferDiscountPerc = 25.0,
                            OfferValidTill = new DateTime(2023, 11, 4, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3901)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
