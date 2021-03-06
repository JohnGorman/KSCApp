﻿// <auto-generated />
using System;
using KSCApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KSCApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180715203056_UpdateLeague")]
    partial class UpdateLeague
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KSCApp.Models.Fixture", b =>
                {
                    b.Property<int>("FixtureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeagueId");

                    b.Property<DateTime>("PlayDate");

                    b.Property<int>("TeamAId");

                    b.Property<int>("TeamBId");

                    b.HasKey("FixtureId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("TeamAId");

                    b.HasIndex("TeamBId");

                    b.ToTable("Fixture");
                });

            modelBuilder.Entity("KSCApp.Models.GameResult", b =>
                {
                    b.Property<int>("GameResultId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameNo");

                    b.Property<int>("MatchId");

                    b.Property<int>("PlayerApoints");

                    b.Property<int>("PlayerBpoints");

                    b.HasKey("GameResultId");

                    b.HasIndex("MatchId");

                    b.ToTable("GameResult");
                });

            modelBuilder.Entity("KSCApp.Models.League", b =>
                {
                    b.Property<int>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<bool>("FixturesMade");

                    b.Property<string>("LeagueName");

                    b.Property<int>("LeagueType");

                    b.Property<int>("NoOfLevels");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("LeagueId");

                    b.ToTable("League");
                });

            modelBuilder.Entity("KSCApp.Models.LevelTime", b =>
                {
                    b.Property<int>("LevelTimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Level");

                    b.Property<TimeSpan>("StartTime");

                    b.HasKey("LevelTimeId");

                    b.ToTable("LevelTime");
                });

            modelBuilder.Entity("KSCApp.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FixtureId");

                    b.Property<int?>("Level");

                    b.Property<bool?>("Played");

                    b.Property<DateTime?>("PlayedDate");

                    b.Property<int>("PlayerAId");

                    b.Property<int>("PlayerAgames");

                    b.Property<int>("PlayerBId");

                    b.Property<int>("PlayerBgames");

                    b.HasKey("MatchId");

                    b.HasIndex("FixtureId");

                    b.HasIndex("PlayerAId");

                    b.HasIndex("PlayerBId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("KSCApp.Models.MatchSlot", b =>
                {
                    b.Property<int>("MatchSlotId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MatchId");

                    b.Property<DateTime>("MyProperty");

                    b.Property<int?>("SlotMinutes");

                    b.HasKey("MatchSlotId");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchSlot");
                });

            modelBuilder.Entity("KSCApp.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNo")
                        .IsRequired();

                    b.Property<string>("KSCAppUserId");

                    b.Property<string>("PlayerName")
                        .IsRequired();

                    b.Property<int>("PlayerStatus");

                    b.Property<int>("PlayerType");

                    b.Property<bool>("PlayingLeague");

                    b.Property<byte[]>("ProfilePicture");

                    b.Property<int?>("Rank");

                    b.Property<string>("UserId");

                    b.HasKey("PlayerId");

                    b.HasIndex("KSCAppUserId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("KSCApp.Models.RankHistory", b =>
                {
                    b.Property<int>("RankHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateChanged");

                    b.Property<int>("NewRank");

                    b.Property<int>("OldRank");

                    b.Property<int>("PlayerId");

                    b.HasKey("RankHistoryId");

                    b.HasIndex("PlayerId");

                    b.ToTable("RankHistory");
                });

            modelBuilder.Entity("KSCApp.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeagueId");

                    b.Property<byte[]>("TeamLogo");

                    b.Property<string>("TeamName");

                    b.Property<int>("TeamNo");

                    b.HasKey("TeamId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("KSCApp.Models.TeamPlayer", b =>
                {
                    b.Property<int>("TeamPlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GamesLost");

                    b.Property<int?>("GamesWon");

                    b.Property<int>("Level");

                    b.Property<int?>("MatchesPlayed");

                    b.Property<int?>("MatchesWon");

                    b.Property<int>("PlayerId");

                    b.Property<int>("TeamId");

                    b.HasKey("TeamPlayerId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KSCApp.Models.KSCAppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNo");

                    b.ToTable("KSCAppUser");

                    b.HasDiscriminator().HasValue("KSCAppUser");
                });

            modelBuilder.Entity("KSCApp.Models.Fixture", b =>
                {
                    b.HasOne("KSCApp.Models.League", "League")
                        .WithMany("Fixtures")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KSCApp.Models.Team", "TeamA")
                        .WithMany()
                        .HasForeignKey("TeamAId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KSCApp.Models.Team", "TeamB")
                        .WithMany()
                        .HasForeignKey("TeamBId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KSCApp.Models.GameResult", b =>
                {
                    b.HasOne("KSCApp.Models.Match", "Match")
                        .WithMany("GameResults")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KSCApp.Models.Match", b =>
                {
                    b.HasOne("KSCApp.Models.Fixture", "Fixture")
                        .WithMany("Matches")
                        .HasForeignKey("FixtureId");

                    b.HasOne("KSCApp.Models.Player", "PlayerA")
                        .WithMany()
                        .HasForeignKey("PlayerAId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KSCApp.Models.Player", "PlayerB")
                        .WithMany()
                        .HasForeignKey("PlayerBId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KSCApp.Models.MatchSlot", b =>
                {
                    b.HasOne("KSCApp.Models.Match", "Match")
                        .WithMany("MatchSlots")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("KSCApp.Models.Player", b =>
                {
                    b.HasOne("KSCApp.Models.KSCAppUser", "KSCAppUser")
                        .WithMany("Players")
                        .HasForeignKey("KSCAppUserId");
                });

            modelBuilder.Entity("KSCApp.Models.RankHistory", b =>
                {
                    b.HasOne("KSCApp.Models.Player", "Player")
                        .WithMany("RankHistory")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KSCApp.Models.Team", b =>
                {
                    b.HasOne("KSCApp.Models.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KSCApp.Models.TeamPlayer", b =>
                {
                    b.HasOne("KSCApp.Models.Player", "Player")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KSCApp.Models.Team", "Team")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
