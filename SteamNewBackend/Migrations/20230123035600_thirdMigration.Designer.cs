﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SteamNewBackend.Database;

#nullable disable

namespace SteamNewBackend.Migrations
{
    [DbContext(typeof(MariaDbContext))]
    [Migration("20230123035600_thirdMigration")]
    partial class thirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SteamNewBackend.Models.DevTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DevTeam_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DevTeams");
                });

            modelBuilder.Entity("SteamNewBackend.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DevTeam_Id")
                        .HasColumnType("int");

                    b.Property<string>("Game_Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Game_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Game_Price")
                        .HasColumnType("int");

                    b.Property<DateTime>("Game_RelDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("SteamNewBackend.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<string>("Post_Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Post_Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Game_Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SteamNewBackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DevTeam_Id")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("User_Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SteamNewBackend.Models.UserGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Game_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserGames");
                });

            modelBuilder.Entity("SteamNewBackend.Models.Post", b =>
                {
                    b.HasOne("SteamNewBackend.Models.Game", "Game")
                        .WithMany("Posts")
                        .HasForeignKey("Game_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("SteamNewBackend.Models.UserGame", b =>
                {
                    b.HasOne("SteamNewBackend.Models.Game", "Game")
                        .WithMany("UserGames")
                        .HasForeignKey("Game_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamNewBackend.Models.User", "User")
                        .WithMany("UserGames")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SteamNewBackend.Models.Game", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("UserGames");
                });

            modelBuilder.Entity("SteamNewBackend.Models.User", b =>
                {
                    b.Navigation("UserGames");
                });
#pragma warning restore 612, 618
        }
    }
}
