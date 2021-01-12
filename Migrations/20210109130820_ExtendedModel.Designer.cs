﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CinemaModel.Data;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20210109130820_ExtendedModel")]
    partial class ExtendedModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Actor", b =>
                {
                    b.Property<int>("ActorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ActorID");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.AppearanceMovie", b =>
                {
                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("ActorID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("AppearanceMovie");
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MovieGenre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Movie", b =>
                {
                    b.Property<int>("MovieID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoxOffice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("FilmDirector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudioID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("StudioID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Studio", b =>
                {
                    b.Property<int>("StudioID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Founded")
                        .HasColumnType("datetime2");

                    b.Property<string>("ParentOrganization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudioName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioID");

                    b.ToTable("Studio");
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.AppearanceMovie", b =>
                {
                    b.HasOne("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Actor", "Actor")
                        .WithMany("AppearanceMovie")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Movie", "Movie")
                        .WithMany("AppearanceMovie")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Movie", b =>
                {
                    b.HasOne("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Category", "Category")
                        .WithMany("Movies")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tomoiaga_AndreiBogdan_CinemaTAB.Models.Studio", "Studio")
                        .WithMany("Movies")
                        .HasForeignKey("StudioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}